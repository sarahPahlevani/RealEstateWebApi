using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Crm
{
    public class RequestAgentController : ModelController<RequestAgent, RequestAgentDto>
    {
        public RequestAgentController(IModelService<RequestAgent, RequestAgentDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<RequestAgent>, IQueryable<RequestAgentDto>> DtoConverter
            => items => items.Select(i => new RequestAgentDto
            {
                Id = i.Id,
                Description = i.Description,
                RequestId = i.RequestId,
                AgentId = i.AgentId,
                IsActive = i.IsActive,
                FromDate = i.FromDate,
                ToDate = i.ToDate
            });
    }
}
