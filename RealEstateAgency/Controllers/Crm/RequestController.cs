using Microsoft.AspNetCore.Authorization;
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
using RealEstateAgency.Shared.Services;
using System;
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
        private readonly IEntityService<RequestAgent> _requestAgentService;
        private readonly IEntityService<UserAccount> _userAccountService;
        private readonly IUpdateSignaler _signaler;

        public RequestController(IModelService<Request, RequestDto> modelService, IFastHasher hasher
            , IUserProvider userProvider, IEntityService<RequestAgent> requestAgentService,
            IUpdateSignaler signaler, IEntityService<UserAccount> userAccountService)
            : base(modelService)
        {
            _hasher = hasher;
            _userProvider = userProvider;
            _requestAgentService = requestAgentService;
            _signaler = signaler;
            _userAccountService = userAccountService;
        }

        public override Func<IQueryable<Request>, IQueryable<RequestDto>> DtoConverter => items => items.Select(i => new RequestDto
        {
            Id = i.Id,
            Description = i.Description,
            Title = i.Title,
            WorkflowId = i.WorkflowId,
            RequestTypeId = i.RequestTypeId,
            MarketingAssistantTrackingCode = i.MarketingAssistantTrackingCode,
            TrackingNumber = i.TrackingNumber,
            UserAccountIdRequester = i.UserAccountIdRequester
        });

        public override Func<IQueryable<Request>, IQueryable<RequestListDto>> PagingConverter
            => items => items.Include(i => i.UserAccountIdRequesterNavigation)
                .Include(i => i.RequestAgent)
                .Include("RequestAction.RequestActionFollowUp")
                .Select(i => new RequestListDto
                {
                    Id = i.Id,
                    Description = i.Description,
                    Title = i.Title,
                    DateCreated = i.DateCreated,
                    RequestTypeId = i.RequestTypeId,
                    CanAddProperty = i.RequestType.CanAddProperty,
                    MarketingAssistantTrackingCode = i.MarketingAssistantTrackingCode,
                    TrackingNumber = i.TrackingNumber,
                    UserAccountIdRequester = i.UserAccountIdRequester,
                    User = i.UserAccountIdRequesterNavigation,
                    Property = i.Property.FirstOrDefault(p => p.RequestId == i.Id),
                    AgentId = i.RequestAgent.Any(r => r.RequestId == i.Id && r.IsActive == true) ?
                        i.RequestAgent.FirstOrDefault(r => r.RequestId == i.Id && r.IsActive == true)
                        .AgentId : -1,
                    Actions = i.RequestAction.ToList(),
                    States = i.RequestState.ToList(),
                    IsAssigned = i.RequestAgent.Any(ra => ra.IsActive == true)
                }).OrderBy(i => i.IsAssigned).ThenByDescending(i => i.DateCreated);

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

        public override async Task<ActionResult<RequestDto>> Create(RequestDto value, CancellationToken cancellationToken)
        {
            value.TrackingNumber = _hasher.CalculateTimeHash("TrackingNumber" + Guid.NewGuid());
            value.DateCreated = DateTime.Now;
            value.UserAccountIdRequester = _userProvider.Id;
            var res = await ModelService.CreateByDtoAsync(value, cancellationToken);
            await _signaler.Signal(_userAccountService.GetAll(u => u.AgentUserAccount.Any(a => a.IsResponsible))
                .Select(u => u.Id).ToList(), nameof(Request), "A new request has arrived");
            return res;
        }

        public override async Task<ActionResult<PageResultDto<RequestListDto>>>
            GetPageAsync([FromBody] PageRequestFilterDto requestDto, CancellationToken cancellationToken)
        {
            var result = await GetPageResultAsync(
                ModelService.AsQueryable(i => i.RequestAgent.Any(ra => ra.AgentId == _userProvider.AgentId && ra.IsActive == true)), requestDto,
                requestDto.Filter.ToObject<RequestListFilter>(),
                cancellationToken);
            foreach (var item in result.Value.Items) item.User.PasswordHash = null;

            return result;
        }

        public override async Task<ActionResult<PageResultDto<RequestListDto>>> GetPageAsync(int pageSize, int pageNumber,
            CancellationToken cancellationToken)
        {
            var result = await GetPageResultAsync(
                ModelService.AsQueryable(i => i.RequestAgent.Any()
                 && i.RequestAgent.FirstOrDefault().AgentId == _userProvider.AgentId
                 && i.RequestAgent.FirstOrDefault().IsActive == true),
                new PageRequestDto(pageSize, pageNumber),
                new NullFilter<Request>(), cancellationToken);
            foreach (var item in result.Value.Items) item.User.PasswordHash = null;

            return result;
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult<PageResultDto<RequestListDto>>> GetOpenRequests([FromBody] PageRequestFilterDto requestDto, CancellationToken cancellationToken)
        {
            if (!_userProvider.IsAgent || _userProvider.IsResponsible is null || _userProvider.IsResponsible == false)
                return Forbid();

            var result = await GetPageResultAsync(
                ModelService.Queryable, requestDto,
                requestDto.Filter.ToObject<RequestListFilter>(),
                cancellationToken);

            foreach (var item in result.Value.Items) item.User.PasswordHash = null;

            return result;
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult> ChangeRequestAgent([FromBody] ChangeRequestAgentDto dto, CancellationToken cancellationToken)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (!_userProvider.IsAgent || _userProvider.IsResponsible is null)
                        return Forbid("Only agents can access this section");

                    var req = await ModelService.Queryable.Include(i => i.RequestAgent)
                        .FirstOrDefaultAsync(r => r.Id == dto.RequestId, cancellationToken);

                    if (req is null) return NotFound();

                    if (!_userProvider.IsResponsible.Value && req.RequestAgent.All(i => i.AgentId != _userProvider.AgentId))
                        return Forbid("You cannot change others request");

                    await RemoveOtherRelations(dto, cancellationToken);

                    await _requestAgentService.CreateAsync(new RequestAgent
                    {
                        AgentId = dto.NewAgentId,
                        FromDate = DateTime.Now,
                        Description = dto.Description,
                        IsActive = true,
                        RequestId = dto.RequestId,
                    }, cancellationToken);

                    await _signaler.Signal(
                        _userAccountService.Get(u => u.AgentUserAccount.Any(a => a.Id == dto.NewAgentId)).Id,
                        nameof(Request),
                        "A new request has been assigned to you");

                    scope.Complete();
                    return NoContent();
                }
                catch (Exception)
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
                agentRequest.ToDate = new DateTime();

                await _requestAgentService.UpdateAsync(agentRequest
                    , cancellationToken);
            }
        }
    }
}
