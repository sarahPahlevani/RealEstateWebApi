using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Crm
{
    public class WorkflowController : ModelController<Workflow, WorkflowDto>
    {
        public WorkflowController(IModelService<Workflow, WorkflowDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<Workflow>, IQueryable<WorkflowDto>> DtoConverter
        => items => items.Select(i => new WorkflowDto
        {
            Id = i.Id,
            Name = i.Name
        });
    }
}
