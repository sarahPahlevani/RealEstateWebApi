using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestStateDto : ModelDtoBase<RequestState>
    {
        public override int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int WorkflowStepId { get; set; }
        public WorkflowStep WorkflowStep { get; set; }
        public DateTime? StartStepDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public int AgentId { get; set; }

        public override IModelDto<RequestState> From(RequestState entity)
        {
            Id = entity.Id;
            RequestId = entity.RequestId;
            Description = entity.Description;
            IsDone = entity.IsDone;
            WorkflowStepId = entity.WorkflowStepId;
            WorkflowStep = entity.WorkflowStep;
            StartStepDate = entity.StartStepDate;
            AgentId = entity.AgentId;

            return this;
        }

        public override RequestState Create() =>
            new RequestState
            {
                Description = Description,
                RequestId = RequestId,
                IsDone = IsDone,
                StartStepDate = StartStepDate,
                FinishedDate = FinishedDate,
                WorkflowStepId = WorkflowStepId,
                AgentId = AgentId,
            };

        public override RequestState Update() =>
            new RequestState
            {
                Id = Id,
                Description = Description,
                RequestId = RequestId,
                IsDone = IsDone,
                StartStepDate = StartStepDate,
                FinishedDate = FinishedDate,
                WorkflowStepId = WorkflowStepId,
                AgentId = AgentId,
            };
    }
}
