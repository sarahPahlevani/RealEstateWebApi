using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Crm
{
    public class WorkflowStepController : ModelController<WorkflowStep, WorkflowStepDto>
    {
        public WorkflowStepController(IModelService<WorkflowStep, WorkflowStepDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<WorkflowStep>, IQueryable<WorkflowStepDto>> DtoConverter
        => items => items.Select(i => new WorkflowStepDto
        {
            Id = i.Id,
            Name = i.Name,
            WorkflowId = i.WorkflowId,
            StepNumber = i.StepNumber
        });
    }
}
