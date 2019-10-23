using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestActionFollowUpDto : ModelDtoBase<RequestActionFollowUp>
    {
        public override int Id { get; set; }

        [Required]
        public int RequestActionId { get; set; }

        [Required]
        public int ActionTypeId { get; set; }

        [Required]
        public int AgentIdFollowUp { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime FollowUpDateTime { get; set; }
        public DateTime? FollowUpDateTimeSnooze { get; set; }
        public bool IsDone { get; set; }

        public override IModelDto<RequestActionFollowUp> From(RequestActionFollowUp entity)
        {
            Id = entity.Id;
            RequestActionId = entity.RequestActionId;
            ActionTypeId = entity.ActionTypeId;
            AgentIdFollowUp = entity.AgentIdFollowUp;
            Description = entity.Description;
            FollowUpDateTime = entity.FollowUpDateTime;
            FollowUpDateTimeSnooze = entity.FollowUpDateTimeSnooze;
            IsDone = entity.IsDone;
            return this;
        }

        public override RequestActionFollowUp Create() =>
            new RequestActionFollowUp
            {
                RequestActionId = RequestActionId,
                ActionTypeId = ActionTypeId,
                AgentIdFollowUp = AgentIdFollowUp,
                Description = Description,
                FollowUpDateTime = FollowUpDateTime,
                FollowUpDateTimeSnooze = FollowUpDateTimeSnooze,
                IsDone = IsDone,
            };

        public override RequestActionFollowUp Update() =>
            new RequestActionFollowUp
            {
                Id = Id,
                RequestActionId = RequestActionId,
                ActionTypeId = ActionTypeId,
                AgentIdFollowUp = AgentIdFollowUp,
                Description = Description,
                FollowUpDateTime = FollowUpDateTime,
                FollowUpDateTimeSnooze = FollowUpDateTimeSnooze,
                IsDone = IsDone,
            };
    }
}
