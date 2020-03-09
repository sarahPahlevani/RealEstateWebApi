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

        [Required]
        public bool IsFinish { get; set; }

        public override IModelDto<WorkflowStep> From(WorkflowStep entity)
        {
            Id = entity.Id;
            WorkflowId = entity.WorkflowId;
            Name = entity.Name;
            StepNumber = entity.StepNumber;
            IsFinish = entity.IsFinish;
            return this;
        }

        public override WorkflowStep Create() =>
            new WorkflowStep
            {
                WorkflowId = WorkflowId,
                Name = Name,
                StepNumber = StepNumber,
                IsFinish = false,
            };

        public override WorkflowStep Update() =>
            new WorkflowStep
            {
                Id = Id,
                WorkflowId = WorkflowId,
                Name = Name,
                StepNumber = StepNumber,
                IsFinish = IsFinish,
            };
    }
}
