using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using RealEstateAgency.Dtos.ModelDtos.CRM;

namespace RealEstateAgency.Controllers.Crm
{
    public class RequestStateController : ModelPagingController<RequestState, RequestStateDto, RequestStateListDto>
    {

        private readonly IEntityService<Request> _requestService;
        private readonly IEntityService<RequestState> _requestStateService;
        private readonly IEntityService<WorkflowStep> _workflowStepService;
        private readonly IEntityService<Commission> _commissionService;

        public RequestStateController(IModelService<RequestState, RequestStateDto> modelService, IEntityService<Request> requestService,
            IEntityService<RequestState> requestStateService, IEntityService<WorkflowStep> workflowStepService,
            IEntityService<Commission> commissionService) : base(modelService)
        {
            _requestService = requestService;
            _requestStateService = requestStateService;
            _workflowStepService = workflowStepService;
            _commissionService = commissionService;
        }

        public override Func<IQueryable<RequestState>, IQueryable<RequestStateDto>> DtoConverter
            => items => items.Select(i => new RequestStateDto
            {
                Id = i.Id,
                RequestId = i.RequestId,
                WorkflowStepId = i.WorkflowStepId,
                Description = i.Description,
                StartStepDate = i.StartStepDate,
                FinishedDate = i.FinishedDate,
                IsDone = i.IsDone,
                AgentId = i.AgentId,
            });

        public override Func<IQueryable<RequestState>, IQueryable<RequestStateListDto>> PagingConverter
            => items => items
                //.Include(i => i.WorkflowStep)
                .Select(i => new RequestStateListDto
                {
                    Id = i.Id,
                    RequestId = i.RequestId,
                    WorkflowStepId = i.WorkflowStepId,
                    WorkflowStep = i.WorkflowStep,
                    Description = i.Description,
                    StartStepDate = i.StartStepDate,
                    FinishedDate = i.FinishedDate,
                    IsDone = i.IsDone,
                    AgentId = i.AgentId,
                    AgentName = $"{i.Agent.UserAccount.FirstName} {i.Agent.UserAccount.LastName}",
                    Agent = i.Agent,
                }).OrderBy(i => i.WorkflowStep.StepNumber);


        [HttpGet("[Action]")]
        public async Task<ActionResult<PageResultDto<RequestStateListDto>>> GetByRequestId(int requestId, CancellationToken cancellationToken)
        {
            //var result = await new PageResultDto<RequestStateListDto>(
            //            ModelService.AsQueryable(r => r.RequestId == requestId)
            //            .Select(p => new RequestStateListDto
            //            {
            //                Id = p.Id,
            //                RequestId = p.RequestId,
            //                WorkflowStepId = p.WorkflowStepId,
            //                WorkflowStep = p.WorkflowStep,
            //                StartStepDate = p.StartStepDate,
            //                FinishedDate = p.FinishedDate,
            //                Description = p.Description,
            //                IsDone = p.IsDone,
            //                AgentId = p.AgentId,
            //            }), new PageRequestDto(100, 1))
            //    .GetPage(cancellationToken);

            var result = await GetPageResultAsync(
                ModelService.AsQueryable(i => i.RequestId == requestId),
                new PageRequestDto(100, 0),
                new NullFilter<RequestState>(), cancellationToken);

            return result;
        }



        public override async Task<ActionResult<RequestStateDto>> Create(RequestStateDto value, CancellationToken cancellationToken)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    var req = _requestService.Get(value.RequestId);
                    if (req is null)
                        throw new Exception("not found request");

                    var wsLatest = _requestStateService.AsQueryable(r => r.RequestId == value.RequestId).Include("WorkflowStep").OrderByDescending(r => r.Id).FirstOrDefault();
                    if (wsLatest is null)
                        throw new Exception("not found latest state");

                    var ws = _workflowStepService.Get(value.WorkflowStepId);
                    if (ws is null)
                        throw new Exception("not found workflowId");

                    if (!wsLatest.WorkflowStep.IsFinish)
                        req.IsDone = value.IsDone;

                    if (ws.IsFinish)
                    {
                        req.IsDone = true;
                        req.IsSuccess = true;

                        var com = new Commission
                        {
                            Id = value.RequestId,
                            CommissionPercent = req.Commission.GetValueOrDefault(0),
                            Amount = (req.PropertyNavigation.PropertyPrice.Price * req.Commission.GetValueOrDefault(0)) / 100,
                            DateCreated = DateTime.UtcNow,
                        };
                        var commissionResult = await _commissionService.CreateAsync(com, cancellationToken);
                    }

                    var newReq = _requestService.Update(req);

                    var res = await ModelService.CreateByDtoAsync(value, cancellationToken);

                    scope.Complete();
                    return res;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }

            }
        }
    }
}
