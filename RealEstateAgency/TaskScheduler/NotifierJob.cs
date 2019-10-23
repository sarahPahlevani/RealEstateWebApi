using Coravel.Events.Interfaces;
using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.NotificationSystem;
using RealEstateAgency.NotificationSystem.NotifyMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RealEstateAgency.NotificationSystem.Signalers;
using RealEstateAgency.Shared.BaseModels;

namespace RealEstateAgency.TaskScheduler
{
    public class NotifierJob : IInvocable, IDisposable
    {
        private readonly IDispatcher _dispatcher;
        private readonly IUpdateSignaler _signaler;
        private readonly RealEstateDbContext _context;
        private List<UserNotifyMessage> _events;
        private readonly IServiceScope _scope;
        private readonly AppSetting _appSetting;

        public NotifierJob(IDispatcher dispatcher, IServiceProvider provider, IUpdateSignaler signaler
            , IOptions<AppSetting> appSettings)
        {
            _dispatcher = dispatcher;
            _signaler = signaler;
            _appSetting = appSettings.Value;
            _scope = provider.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<RealEstateDbContext>();
        }

        private async Task ProvideUserEvents()
        {
            _events = await _context.RequestActionFollowUp
                .Where(r => !r.IsDone && (DateTime.Now >= r.FollowUpDateTime
                                          || (r.FollowUpDateTimeSnooze != null &&
                                              DateTime.Now >= r.FollowUpDateTimeSnooze)))
                .Select(i => new UserNotifyMessage
                {
                    Id = i.Id,
                    ActionTypeName = i.ActionType.Name,
                    Description = i.Description,
                    UserId = i.AgentIdFollowUpNavigation.UserAccount.Id,
                    Username = i.AgentIdFollowUpNavigation.UserAccount.UserName,
                    UserEmail = i.AgentIdFollowUpNavigation.UserAccount.Email,
                    AgentId = i.AgentIdFollowUpNavigation.Id,
                    RequestActionId = i.RequestActionId,
                    RequestId = i.RequestActionNavigation.RequestId,
                    Type = DateTime.Now >= i.FollowUpDateTime ? UserEventType.Todo : UserEventType.Snooze,
                    ActionTypeId = i.ActionTypeId,
                    Firstname = i.AgentIdFollowUpNavigation.UserAccount.FirstName,
                    Fullname = i.AgentIdFollowUpNavigation.UserAccount.FirstName +
                               i.AgentIdFollowUpNavigation.UserAccount.MiddleName +
                               i.AgentIdFollowUpNavigation.UserAccount.LastName,
                    RequestUrl = _appSetting.DashboardBaseUrl + "en/agent/requests",
                    BaseUrl = _appSetting.DashboardBaseUrl,
                    IsDone = false,
                    FollowUpDateTimeSnooze = i.FollowUpDateTimeSnooze,
                    FollowUpDateTime = i.FollowUpDateTime,
                    AgentIdFollowUp = i.AgentIdFollowUp,
                })
                .ToListAsync();
        }

        public async Task Invoke()
        {
            await ProvideUserEvents();
            if (_events.Count == 0) return;

            foreach (var userNotifyMessage in _events)
                await _dispatcher.Broadcast(new NotifyUserEvent(userNotifyMessage));
            await CreateNewActions();
            await UpdateActionsFollowUps();
        }

        private async Task UpdateActionsFollowUps()
        {
            var ids = _events.Select(i => i.Id).ToList();
            var actionFollowUps = _context.RequestActionFollowUp
                    .Where(i => !i.IsDone && ids.Contains(i.Id)).ToList();

            var doneTasks = _events.Where(i => i.Type == UserEventType.Todo)
                .Select(i => i.Id).ToList();

            var snoozedTasks = _events.Where(i => i.Type == UserEventType.Snooze)
                .Select(i => i.Id).ToList();

            actionFollowUps.ForEach(a =>
            {
                if (doneTasks.Contains(a.Id))
                    a.IsDone = true;
                if (snoozedTasks.Contains(a.Id))
                    a.FollowUpDateTimeSnooze = null;
            });

            _context.RequestActionFollowUp.UpdateRange(actionFollowUps);
            await _context.SaveChangesAsync();
        }

        private async Task CreateNewActions()
        {
            var toCreate = _events.Where(i => i.Type == UserEventType.Todo).ToList();
            if (toCreate.Count == 0) return;
            await _context.RequestAction.AddRangeAsync(
                toCreate.Select(e => new RequestAction
                {
                    ActionDate = DateTime.Now,
                    ActionTypeId = e.ActionTypeId,
                    AgentId = e.AgentId,
                    Description = e.Description,
                    MetaDataJson = "{}",
                    RequestId = e.RequestId,
                    ActionTime = "",
                    RequestActionFollowUpReference = e.Id,
                    ActionIsSuccess = false,
                }
                    ).ToList()
                );
            await _context.SaveChangesAsync();
            await _signaler.Signal(toCreate.Select(i => i.UserId).Distinct().ToList()
                , nameof(RequestAction), "New Action created for you");
        }

        public void Dispose()
        {
            _context.Dispose();
            _scope.Dispose();
        }
    }
}
