using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class WorkflowStepDto : ModelDtoBase<WorkflowStep>
    {
        public override int Id { get; set; }

        [Required]
        public int WorkflowId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int StepNumber { get; set; }

        public override IModelDto<WorkflowStep> From(WorkflowStep entity)
        {
            Id = entity.Id;
            WorkflowId = entity.WorkflowId;
            Name = entity.Name;
            StepNumber = entity.StepNumber;
            return this;
        }

        public override WorkflowStep Create() =>
            new WorkflowStep
            {
                Name = Name,
                StepNumber = StepNumber,
                WorkflowId = WorkflowId
            };

        public override WorkflowStep Update() =>
            new WorkflowStep
            {
                Id = Id,
                Name = Name,
                StepNumber = StepNumber,
                WorkflowId = WorkflowId
            };
    }
}
