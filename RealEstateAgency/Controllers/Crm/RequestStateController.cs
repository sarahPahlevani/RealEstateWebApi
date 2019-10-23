using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Crm
{
    public class RequestStateController : ModelController<RequestState, RequestStateDto>
    {
        public RequestStateController(IModelService<RequestState, RequestStateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<RequestState>, IQueryable<RequestStateDto>> DtoConverter
            => items => items.Select(i => new RequestStateDto
            {
                Id = i.Id,
                Description = i.Description,
                RequestId = i.RequestId,
                StartStepDate = i.StartStepDate,
                WorkflowStepId = i.WorkflowStepId
            });
    }
}
