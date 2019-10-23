using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestActionDto : ModelDtoBase<RequestAction>
    {
        public override int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int ActionTypeId { get; set; }

        [Required]
        public int AgentId { get; set; }

        public int? RequestActionFollowUpReference { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime ActionDate { get; set; }
        public string ActionTime { get; set; }
        public bool? ActionIsSuccess { get; set; }
        public string MetaDataJson { get; set; }

        public override IModelDto<RequestAction> From(RequestAction entity)
        {
            Id = entity.Id;
            RequestId = entity.RequestId;
            ActionTypeId = entity.ActionTypeId;
            AgentId = entity.AgentId;
            RequestActionFollowUpReference = entity.RequestActionFollowUpReference;
            Description = entity.Description;
            ActionDate = entity.ActionDate;
            ActionTime = entity.ActionTime;
            ActionIsSuccess = entity.ActionIsSuccess;
            MetaDataJson = entity.MetaDataJson;
            return this;
        }

        public override RequestAction Create() =>
            new RequestAction
            {
                RequestId = RequestId,
                ActionTypeId = ActionTypeId,
                AgentId = AgentId,
                RequestActionFollowUpReference = RequestActionFollowUpReference,
                Description = Description,
                ActionDate = ActionDate,
                ActionTime = ActionTime,
                ActionIsSuccess = ActionIsSuccess,
                MetaDataJson = MetaDataJson,
            };

        public override RequestAction Update() =>
            new RequestAction
            {
                RequestId = RequestId,
                ActionTypeId = ActionTypeId,
                AgentId = AgentId,
                RequestActionFollowUpReference = RequestActionFollowUpReference,
                Description = Description,
                ActionDate = ActionDate,
                ActionTime = ActionTime,
                ActionIsSuccess = ActionIsSuccess,
                MetaDataJson = MetaDataJson,
                Id = Id
            };
    }
}
