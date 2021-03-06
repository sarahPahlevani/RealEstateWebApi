﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.NotificationSystem.Signalers;
using RealEstateAgency.Shared.Exceptions;
using RealEstateAgency.Shared.Services;
using RealEstateAgency.Shared.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace RealEstateAgency.Controllers.Crm
{
    public class RequestController : ModelPagingController<Request, RequestDto, RequestListDto>
    {
        private readonly IFastHasher _hasher;
        private readonly IUserProvider _userProvider;
        private readonly IEntityService<RequestState> _requestStateService;
        private readonly IEntityService<RequestAgent> _requestAgentService;
        private readonly IEntityService<Property> _propertyService;
        private readonly IEntityService<Workflow> _workflowService;
        private readonly IEntityService<WorkflowStep> _workflowStepService;
        private readonly IEntityService<UserAccount> _userAccountService;
        private readonly IUpdateSignaler _signaler;

        public RequestController(IModelService<Request, RequestDto> modelService, IFastHasher hasher,
            IUserProvider userProvider, IEntityService<RequestState> requestStateService, IEntityService<RequestAgent> requestAgentService,
            IEntityService<Property> propertyService, IEntityService<Workflow> workflowService, IEntityService<WorkflowStep> workflowStepService,
            IUpdateSignaler signaler, IEntityService<UserAccount> userAccountService)
            : base(modelService)
        {
            _hasher = hasher;
            _userProvider = userProvider;
            _requestStateService = requestStateService;
            _requestAgentService = requestAgentService;
            _propertyService = propertyService;
            _workflowService = workflowService;
            _workflowStepService = workflowStepService;
            _signaler = signaler;
            _userAccountService = userAccountService;
        }

        public override Func<IQueryable<Request>, IQueryable<RequestDto>> DtoConverter => items => items
            .Include(i => i.UserAccountIdRequesterNavigation)
            .Include(i => i.RequestAgent)
            .Include(i => i.RequestType)
            .Include("RequestAction.RequestActionFollowUp")
            .Select(i => new RequestDto
            {
                Id = i.Id,
                Description = i.Description,
                Title = i.Title,
                DateCreated = i.DateCreated,
                RequestTypeId = i.RequestTypeId,
                RequestType = i.RequestType,
                WorkflowId = i.WorkflowId,
                Workflow = i.Workflow != null ? new Workflow
                {
                    Id = i.Workflow.Id,
                    Name = i.Workflow.Name,
                    RequestTypeId = i.Workflow.RequestTypeId,
                    RequestType = i.Workflow.RequestType,
                    Request = i.Workflow.Request,
                    WorkflowStep = i.Workflow.WorkflowStep,
                } : null,
                CanAddProperty = i.RequestType.CanAddProperty,
                UserAccountIdShared = i.UserAccountIdShared,
                NetworkIdShared = i.NetworkIdShared,
                TrackingNumber = i.TrackingNumber,
                UserAccountIdRequester = i.UserAccountIdRequester,
                RequesterFullname = i.RequesterFullname,
                RequesterEmail = i.RequesterEmail,
                RequesterPhone = i.RequesterPhone,
                User = i.UserAccountIdRequesterNavigation,
                PropertyId = i.PropertyId,
                PropertyTitle = i.PropertyNavigation != null ? i.PropertyNavigation.Title : "",
                AgentId = i.AgentId,
                Agent = i.Agent,
                Actions = i.RequestAction,
                States = i.RequestState.Select(r => new RequestState
                {
                    Id = r.Id,
                    RequestId = r.RequestId,
                    Request = r.Request,
                    StartStepDate = r.StartStepDate,
                    FinishedDate = r.FinishedDate,
                    Description = r.Description,
                    AgentId = r.AgentId,
                    Agent = r.Agent,
                    WorkflowStepId = r.WorkflowStepId,
                    WorkflowStep = r.WorkflowStep,
                    IsDone = r.IsDone,
                }),
                IsAssigned = i.AgentId.HasValue,
                Commission = i.Commission,
                IsDone = i.IsDone,
                IsSuccess = i.IsSuccess,
                //Id = i.Id,
                //Description = i.Description,
                //Title = i.Title,
                //DateCreated = i.DateCreated,
                //AgentId = i.AgentId,
                //Agent = i.Agent,
                //WorkflowId = i.WorkflowId,
                //RequestTypeId = i.RequestTypeId,
                //RequestType = i.RequestType,
                ////MarketingAssistantTrackingCode = i.MarketingAssistantTrackingCode,
                //UserAccountIdShared = i.UserAccountIdShared,
                //TrackingNumber = i.TrackingNumber,
                //UserAccountIdRequester = i.UserAccountIdRequester,
                //PropertyId = i.PropertyId,
            });

        public override Func<IQueryable<Request>, IQueryable<RequestListDto>> PagingConverter
            => items => items
                .Include(i => i.UserAccountIdRequesterNavigation)
                .Include(i => i.UserAccountIdSharedNavigation)
                .Include(i => i.RequestAgent)
                .Include(i => i.RequestType)
                .Include(i => i.Workflow)
                .Include(i => i.Workflow.WorkflowStep)
                .Include(i => i.RequestState)
                .Include("RequestAction.RequestActionFollowUp")
                .Select(i => new RequestListDto
                {
                    Id = i.Id,
                    RequestTypeId = i.RequestTypeId,
                    RequestTypeName = i.RequestType.Name,
                    WorkflowId = i.WorkflowId,
                    CanAddProperty = i.RequestType.CanAddProperty,
                    UserAccountIdShared = i.UserAccountIdShared,
                    UserAccountSharedFullname = i.UserAccountIdSharedNavigation == null ? "" : $"{i.UserAccountIdSharedNavigation.FirstName} {i.UserAccountIdSharedNavigation.LastName}",
                    NetworkIdShared = i.NetworkIdShared,
                    NetworkSharedName = i.NetworkIdSharedNavigation == null ? "" : i.NetworkIdSharedNavigation.Name,
                    UserAccountIdRequester = i.UserAccountIdRequester,
                    RequesterFullname = i.UserAccountIdRequesterNavigation == null ? i.RequesterFullname : $"{i.UserAccountIdRequesterNavigation.FirstName} {i.UserAccountIdRequesterNavigation.LastName}",
                    RequesterEmail = i.UserAccountIdRequesterNavigation == null ? i.RequesterEmail : i.UserAccountIdRequesterNavigation.Email,
                    RequesterPhone = i.UserAccountIdRequesterNavigation == null ? i.RequesterPhone : i.UserAccountIdRequesterNavigation.Phone01,
                    TrackingNumber = i.TrackingNumber,
                    Title = i.Title,
                    Description = i.Description,
                    AgentId = i.AgentId,
                    AgentName = $"{i.Agent.UserAccount.FirstName} {i.Agent.UserAccount.LastName}",
                    PropertyId = i.PropertyId,
                    PropertyTitle = i.PropertyNavigation != null ? i.PropertyNavigation.Title : "",
                    LatestStateName = i.RequestState.OrderByDescending(rs => rs.Id).FirstOrDefault().WorkflowStep.Name,
                    LatestStateIsFinish = i.RequestState.OrderByDescending(rs => rs.Id).FirstOrDefault().WorkflowStep.IsFinish,
                    IsAssigned = i.AgentId.HasValue,
                    DateCreated = i.DateCreated,
                    IsDone = i.IsDone,
                    IsSuccess = i.IsSuccess,
                    Commission = i.Commission,

                    //Workflow = i.Workflow,
                    //Workflow = i.Workflow != null ? new Workflow
                    //{
                    //    Id = i.Workflow.Id,
                    //    Name = i.Workflow.Name,
                    //    RequestTypeId = i.Workflow.RequestTypeId,
                    //    RequestType = i.Workflow.RequestType,
                    //    Request = i.Workflow.Request,
                    //    WorkflowStep = i.Workflow.WorkflowStep,
                    //} : null,
                    //UserAccountShared = i.UserAccountIdSharedNavigation,
                    //NetworkShared = i.NetworkIdSharedNavigation,
                    //User = i.UserAccountIdRequesterNavigation,
                    //Actions = i.RequestAction,
                    //States = i.RequestState,
                    //States = i.RequestState.Select(r => new RequestState
                    //{
                    //    Id = r.Id,
                    //    RequestId = r.RequestId,
                    //    Request = r.Request,
                    //    StartStepDate = r.StartStepDate,
                    //    FinishedDate = r.FinishedDate,
                    //    Description = r.Description,
                    //    AgentId = r.AgentId,
                    //    Agent = r.Agent,
                    //    WorkflowStepId = r.WorkflowStepId,
                    //    WorkflowStep = r.WorkflowStep,
                    //    IsDone = r.IsDone,
                    //}),
                }).OrderBy(i => i.IsAssigned).ThenByDescending(i => i.DateCreated);

        public override async Task<ActionResult<RequestDto>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var result = await ModelService.AsQueryable(r => r.Id == id)
            //.Include(i => i.UserAccountIdRequesterNavigation)
            .Include(i => i.RequestAgent)
            .Include(i => i.RequestType)
            //.Include("RequestAction.RequestActionFollowUp")
            .Select(i => new RequestDto
            {
                Id = i.Id,
                Description = i.Description,
                Title = i.Title,
                DateCreated = i.DateCreated,
                RequestTypeId = i.RequestTypeId,
                RequestType = i.RequestType,
                WorkflowId = i.WorkflowId,
                Workflow = i.Workflow != null ? new Workflow
                {
                    Id = i.Workflow.Id,
                    Name = i.Workflow.Name,
                    RequestTypeId = i.Workflow.RequestTypeId,
                    RequestType = i.Workflow.RequestType,
                    Request = i.Workflow.Request,
                    WorkflowStep = i.Workflow.WorkflowStep,
                } : null,
                CanAddProperty = i.RequestType.CanAddProperty,
                UserAccountIdShared = i.UserAccountIdShared,
                UserAccountShared = i.UserAccountIdSharedNavigation,
                NetworkIdShared = i.NetworkIdShared,
                NetworkShared = i.NetworkIdSharedNavigation,
                TrackingNumber = i.TrackingNumber,
                UserAccountIdRequester = i.UserAccountIdRequester,
                User = i.UserAccountIdRequesterNavigation,
                RequesterFullname = i.RequesterFullname,
                RequesterEmail = i.RequesterEmail,
                RequesterPhone = i.RequesterPhone,
                PropertyId = i.PropertyId,
                PropertyTitle = i.PropertyNavigation != null ? i.PropertyNavigation.Title : "",
                AgentId = i.AgentId,
                Agent = i.Agent,
                Actions = i.RequestAction,
                States = i.RequestState.Select(r => new RequestState
                {
                    Id = r.Id,
                    RequestId = r.RequestId,
                    Request = r.Request,
                    StartStepDate = r.StartStepDate,
                    FinishedDate = r.FinishedDate,
                    Description = r.Description,
                    AgentId = r.AgentId,
                    Agent = r.Agent,
                    WorkflowStepId = r.WorkflowStepId,
                    WorkflowStep = r.WorkflowStep,
                    IsDone = r.IsDone,
                }),
                IsAssigned = i.AgentId.HasValue,
                Commission = i.Commission,
                IsDone = i.IsDone,
                IsSuccess = i.IsSuccess,
            }).FirstOrDefaultAsync();

            return result;
        }

        [AllowAnonymous]
        [HttpPost("[Action]")]
        public async Task<ActionResult<RequestDto>> CreateRequestSandBox([FromBody]RequestDto request, CancellationToken cancellationToken)
        {
            request.TrackingNumber = _hasher.CalculateTimeHash("TrackingNumber" + Guid.NewGuid());
            request.DateCreated = DateTime.Now;
            var res = await ModelService.CreateByDtoAsync(request, cancellationToken);
            await _signaler.Signal(_userAccountService.GetAll(u => u.AgentUserAccount.Any(a => a.IsResponsible))
                .Select(u => u.Id).ToList(), nameof(Request), "A new request has arrived");
            return res;
        }

        [AllowAnonymous]
        public override async Task<ActionResult<RequestDto>> Create(RequestDto value, CancellationToken cancellationToken)
        {
            var workflow = _workflowService.Queryable.FirstOrDefault(r => r.RequestTypeId == value.RequestTypeId);
            if (workflow is null)
                throw new Exception("not found workflow of this request");
            value.IsDone = false;
            value.IsSuccess = false;
            value.WorkflowId = workflow.Id;
            value.TrackingNumber = _hasher.CalculateTimeHash("TrackingNumber" + Guid.NewGuid());
            value.DateCreated = DateTime.UtcNow;

            if (value.PropertyId.HasValue)
            {
                var property = _propertyService.Queryable.FirstOrDefault(r => r.Id == value.PropertyId.Value);
                if (property is null)
                    throw new Exception("not found property of this request");
                value.Commission = property.Commission;
            }

            var res = await ModelService.CreateByDtoAsync(value, cancellationToken);

            await _signaler.Signal(_userAccountService.GetAll(u => u.AgentUserAccount.Any(a => a.IsResponsible))
                .Select(u => u.Id).ToList(), nameof(Request), "A new request has arrived");

            return res;
        }

        public override async Task<ActionResult<PageResultDto<RequestListDto>>> GetPageAsync([FromBody] PageRequestFilterDto requestDto, CancellationToken cancellationToken)
        {
            var result = await GetPageResultAsync(
                ModelService.AsQueryable(i => i.AgentId == _userProvider.AgentId),
                requestDto,
                requestDto.Filter.ToObject<RequestListFilter>(),
                cancellationToken);
            //foreach (var item in result.Value.Items)
            //{
            //    if (item.User != null)
            //        item.User.PasswordHash = null;
            //}

            return result;
        }

        public override async Task<ActionResult<PageResultDto<RequestListDto>>> GetPageAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            var result = await GetPageResultAsync(
                ModelService.AsQueryable(i => i.AgentId == _userProvider.AgentId),
                //.Include(r => r.RequestAgent)
                //.Include(r => r.RequestType)
                //.Include(r => r.UserAccountIdRequesterNavigation)
                //.Include(r => r.UserAccountIdSharedNavigation)
                //.Include(r => r.PropertyNavigation)
                //.Include(r => r.RequestState)
                //.Include(r => r.RequestAction)
                //.Include(r => r.Workflow)
                //.Include(r => r.Workflow.WorkflowStep),
                new PageRequestDto(pageSize, pageNumber),
                new NullFilter<Request>(), cancellationToken);
            //foreach (var item in result.Value.Items)
            //{
            //    if (item.User != null)
            //        item.User.PasswordHash = null;
            //}

            return result;
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult<PageResultDto<RequestListDto>>> GetOpenRequests([FromBody] PageRequestFilterDto requestDto, CancellationToken cancellationToken)
        {
            if (_userProvider.Role != UserGroups.Administrator)
                if (!_userProvider.IsAgent || _userProvider.IsResponsible.GetValueOrDefault(false) == false)
                    return Forbid();

            var result = await GetPageResultAsync(ModelService.Queryable, requestDto, requestDto.Filter.ToObject<RequestListFilter>(), cancellationToken);
            //foreach (var item in result.Value.Items)
            //{
            //    if (item.User != null)
            //        item.User.PasswordHash = null;
            //}

            return result;
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult> ChangeRequestAgent([FromBody] ChangeRequestAgentDto dto, CancellationToken cancellationToken)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (!_userProvider.IsAgent || !_userProvider.IsResponsible.GetValueOrDefault(false))
                        return Forbid("Only agents can access this section");

                    var req = await ModelService.Queryable.Include(i => i.RequestAgent).Include(i => i.RequestState)
                        .FirstOrDefaultAsync(r => r.Id == dto.RequestId, cancellationToken);

                    if (req is null) return NotFound();

                    if (!_userProvider.IsResponsible.GetValueOrDefault(false) && req.RequestAgent.All(i => i.AgentId != _userProvider.AgentId))
                        return Forbid("You cannot change others request");

                    await RemoveOtherRelations(dto, cancellationToken);

                    req.AgentId = dto.NewAgentId;
                    await ModelService.UpdateAsync(req, cancellationToken);

                    var haveStates = _requestStateService.AsQueryable(r => r.RequestId == req.Id).Any();
                    if (!haveStates)
                    {
                        var firstStep = _workflowStepService.AsQueryable(r => r.WorkflowId == req.WorkflowId).OrderBy(r => r.StepNumber).FirstOrDefault().Id;
                        await _requestStateService.CreateAsync(new RequestState
                        {
                            RequestId = req.Id,
                            WorkflowStepId = firstStep,
                            StartStepDate = DateTime.UtcNow,
                            IsDone = false,
                            AgentId = dto.NewAgentId,
                        });
                    }

                    //await _requestAgentService.CreateAsync(new RequestAgent
                    //{
                    //    AgentId = dto.NewAgentId,
                    //    FromDate = DateTime.UtcNow,
                    //    Description = dto.Description,
                    //    IsActive = true,
                    //    RequestId = dto.RequestId,
                    //}, cancellationToken);

                    await _signaler.Signal(
                        _userAccountService.Get(u => u.AgentUserAccount.Any(a => a.Id == dto.NewAgentId)).Id,
                        nameof(Request),
                        "A new request has been assigned to you");

                    scope.Complete();
                    return Ok();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }

        private async Task RemoveOtherRelations(ChangeRequestAgentDto dto, CancellationToken cancellationToken)
        {
            var agentRequest = await _requestAgentService
                .GetAsync(i => i.AgentId == _userProvider.AgentId.Value && i.RequestId == dto.RequestId
                                                                        && i.IsActive == true, cancellationToken);
            if (agentRequest != null)
            {
                agentRequest.IsActive = false;
                agentRequest.ToDate = DateTime.UtcNow;

                await _requestAgentService.UpdateAsync(agentRequest
                    , cancellationToken);
            }
        }
    }
}
