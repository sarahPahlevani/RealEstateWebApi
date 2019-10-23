using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Mailables;
using RealEstateAgency.NotificationSystem.NotifyMessages;
using RealEstateAgency.Shared.BaseModels;

namespace RealEstateAgency.Controllers.Other
{
    [ApiController, AllowAnonymous,
    Route("[controller]")]
    public class TestMailController : ControllerBase
    {
        private readonly IMailer _mailer;
        private readonly IEntityService<RequestAction> _entityService;
        private readonly AppSetting _appSetting;

        public TestMailController(IMailer mailer, IOptions<AppSetting> appSetting, IEntityService<RequestAction> entityService
            , IUserProvider userProvider)
        {
            _mailer = mailer;
            _entityService = entityService;
            _appSetting = appSetting.Value;
            if (userProvider.AgentId != null)
                _entityService.SetBaseFilter(q => q.Where(i => i.AgentId == userProvider.AgentId));
        }

        [HttpGet]
        public async Task<ContentResult> Index()
        {
            return Content(await _mailer.RenderAsync(new UserEventEmail(
                new UserNotifyMessage
                {
                    Description = "some description goes here",
                    RequestId = 4,
                    Username = "miladbonak",
                    ActionTypeId = 3,
                    AgentId = 6,
                    RequestActionId = 8,
                    Type = UserEventType.Snooze,
                    UserId = 23,
                    UserEmail = "miladbonak@gmail.com",
                    Id = 12,
                    ActionTypeName = "some action",
                    Firstname = "milad",
                    Fullname = "milad bonakdar",
                    RequestUrl = "https://docs.coravel.net/Mailing/",
                    BaseUrl = "https://docs.coravel.net/Mailing/",
                    FollowUpDateTimeSnooze = DateTime.Now,
                    IsDone = false,
                    FollowUpDateTime = DateTime.Now,
                    AgentIdFollowUp = 3,
                }, _appSetting.AdminEmailAddress)), "text/html");
        }

        [HttpGet("[Action]")]
        public async Task<IEnumerable<RequestAction>> Test(CancellationToken cancellationToken)
        {
            return await _entityService.GetAllAsync(cancellationToken);
        }
    }
}
