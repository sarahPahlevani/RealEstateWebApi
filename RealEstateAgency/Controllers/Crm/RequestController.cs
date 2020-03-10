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
        private readonly IEntityService<WorkflowStep> _workflowStepService;
        private readonly IEntityService<UserAccount> _userAccountService;
        private readonly IUpdateSignaler _signaler;

        public RequestController(IModelService<Request, RequestDto> modelService, IFastHasher hasher
            , IUserProvider userProvider, IEntityService<RequestState> requestStateService, IEntityService<RequestAgent> requestAgentService,
            IEntityService<WorkflowStep> workflowStepService, IUpdateSignaler signaler, IEntityService<UserAccount> userAccountService)
            : base(modelService)
        {
            _hasher = hasher;
            _userProvider = userProvider;
            _requestStateService = requestStateService;
            _requestAgentService = requestAgentService;
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
                .Include(i => i.RequestAgent)
                .Include(i => i.RequestType)
                .Include("RequestAction.RequestActionFollowUp")
                .Select(i => new RequestListDto
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
                    RequesterFullname = i.RequesterFullname,
                    RequesterEmail = i.RequesterEmail,
                    RequesterPhone = i.RequesterPhone,
                    User = i.UserAccountIdRequesterNavigation,
                    PropertyId = i.PropertyId,
                    PropertyTitle = i.PropertyNavigation != null ? i.PropertyNavigation.Title : "",
                    AgentId = i.AgentId,
                    AgentName = $"{i.Agent.UserAccount.FirstName} {i.Agent.UserAccount.LastName}",
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
        public async Task<ActionResult<RequestDto>> CreateRequestSandBox(
            [FromBody]RequestDto request, CancellationToken cancellationToken)
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
            var workflow = new RealEstateDbContext().Workflow.FirstOrDefault(r => r.RequestTypeId == value.RequestTypeId);
            if (workflow is null)
                throw new Exception("not found workflow of this request");
            value.IsDone = false;
            value.IsSuccess = false;
            value.WorkflowId = workflow.Id;
            value.TrackingNumber = _hasher.CalculateTimeHash("TrackingNumber" + Guid.NewGuid());
            value.DateCreated = DateTime.UtcNow;

            if (value.PropertyId.HasValue)
            {
                var property = new RealEstateDbContext().Property.FirstOrDefault(r => r.Id == value.PropertyId.Value);
                if (property is null)
                    throw new Exception("not found property of this request");
                value.Commission = property.Commission;
            }

            var res = await ModelService.CreateByDtoAsync(value, cancellationToken);

            await _signaler.Signal(_userAccountService.GetAll(u => u.AgentUserAccount.Any(a => a.IsResponsible))
                .Select(u => u.Id).ToList(), nameof(Request), "A new request has arrived");

            return res;
        }

        public override async Task<ActionResult<PageResultDto<RequestListDto>>>
            GetPageAsync([FromBody] PageRequestFilterDto requestDto, CancellationToken cancellationToken)
        {
            var result = await GetPageResultAsync(
                ModelService.AsQueryable(i => i.AgentId == _userProvider.AgentId),
                requestDto,
                requestDto.Filter.ToObject<RequestListFilter>(),
                cancellationToken);
            foreach (var item in result.Value.Items)
            {
                if (item.User != null)
                    item.User.PasswordHash = null;
            }

            return result;
        }

        public override async Task<ActionResult<PageResultDto<RequestListDto>>> GetPageAsync(int pageSize, int pageNumber,
            CancellationToken cancellationToken)
        {
            var result = await GetPageResultAsync(
                ModelService.AsQueryable(i => i.AgentId == _userProvider.AgentId),
                new PageRequestDto(pageSize, pageNumber),
                new NullFilter<Request>(), cancellationToken);
            foreach (var item in result.Value.Items)
            {
                if (item.User != null)
                    item.User.PasswordHash = null;
            }

            return result;
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult<PageResultDto<RequestListDto>>> GetOpenRequests([FromBody] PageRequestFilterDto requestDto, CancellationToken cancellationToken)
        {
            if (_userProvider.Role != UserGroups.Administrator)
                if (!_userProvider.IsAgent || _userProvider.IsResponsible.GetValueOrDefault(false) == false)
                    return Forbid();

            var result = await GetPageResultAsync(ModelService.Queryable, requestDto, requestDto.Filter.ToObject<RequestListFilter>(), cancellationToken);
            foreach (var item in result.Value.Items)
            {
                if (item.User != null)
                    item.User.PasswordHash = null;
            }

            return result;
        }


        //[Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        //[HttpPost("[Action]")]
        //public async Task<ActionResult<List<CommissionDto>>> NewGetUserCommission()
        //{
        //    List<CommissionDto> list = new List<CommissionDto>();
        //    var result = await ModelService.AsQueryable(t => t.IsDone == true && t.IsSuccess == true && t.UserAccountIdShared != null)
        //        .Include(t => t.Property).Include(i => i.Property.FirstOrDefault().PropertyPrice)
        //        .Select(o => new CommissionDto
        //        {
        //            CommissionPercent = o.Commission,
        //            DateCreated = o.DateCreated,
        //            PropertyId = o.Id,
        //            PropertyPrice = o.Property.FirstOrDefault(t => t.Id == o.PropertyId).PropertyPrice.Price,
        //            PropertyTitle = o.Title,
        //            RequesterFullname = o.RequesterFullname,
        //            TotalCommission = (o.Property.FirstOrDefault(t => t.Id == o.PropertyId).PropertyPrice.Price - ((o.Property.FirstOrDefault(t => t.Id == o.PropertyId).PropertyPrice.Price * (o.Commission == null ? 1 : o.Commission)) / 100)),
        //            UserName = ""
        //        }).ToListAsync();
        //    return result;
        //}


        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        [HttpPost("[Action]")]
        public async Task<ActionResult<List<CommissionDto>>> NewGetUserCommission()
        {

            //SELECT R.RequesterFullname , P.Title , p.Commission , R.Commission , PP.Price , A.UserName , A.FirstName , A.LastName , R.DateCreated FROM CRM.Request R
            //INNER JOIN Estate.Property P ON R.PropertyId = P.Id
            //INNER JOIN RBAC.UserAccount A ON R.UserAccountId_Shared = A.Id
            //INNER JOIN Estate.PropertyPrice PP ON PP.Id = P.Id
            //WHERE R.IsDone = 1 and R.IsSuccess = 1 and R.UserAccountId_Shared is not NULL


            //ModelService.AsQueryable(t => t.RequestAgent).Select(t => t.Property);

            List<CommissionDto> list = new List<CommissionDto>();
            list = await (from r in ModelService.DbContext.Request
                    join p in ModelService.DbContext.Property on r.PropertyId equals p.Id
                    join u in ModelService.DbContext.UserAccount on r.UserAccountIdShared equals u.Id
                    join pp in ModelService.DbContext.PropertyPrice on r.PropertyId equals pp.Id
                    where r.UserAccountIdShared != null && r.IsDone == true && r.IsSuccess == true
                    //&& r.UserAccountIdShared == _userProvider.Id
                    select new CommissionDto
                    {
                        CommissionPercent = r.Commission == null ? 0 : r.Commission,
                        PropertyId = p.Id,
                        PropertyPrice = pp.Price,
                        PropertyTitle = p.Title,
                        TotalCommission = pp.Price - (pp.Price - ((pp.Price * (r.Commission == null ? 0 : r.Commission)) / 100)),
                        DateCreated = r.DateCreated,
                        RequesterFullname = r.RequesterFullname,
                        UserName = u.UserName,
                        CurrencySymbol = pp.Currency.Symbol
                    }).ToListAsync();

            return list;
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
