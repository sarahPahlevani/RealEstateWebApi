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

namespace RealEstateAgency.Controllers.Crm
{
    public class RequestStateController : ModelPagingController<RequestState, RequestStateDto, RequestStateListDto>
    {
        public RequestStateController(IModelService<RequestState, RequestStateDto> modelService) : base(modelService)
        {
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
                .Include(i => i.WorkflowStep)
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
                new PageRequestDto(100, 1),
                new NullFilter<RequestState>(), cancellationToken);

            return result;
        }

    }
}
