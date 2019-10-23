using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestAgentDto : ModelDtoBase<RequestAgent>
    {
        public override int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int AgentId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsActive { get; set; }

        [Required]
        public string Description { get; set; }

        public override IModelDto<RequestAgent> From(RequestAgent entity)
        {
            Id = entity.Id;
            RequestId = entity.RequestId;
            AgentId = entity.AgentId;
            FromDate = entity.FromDate;
            ToDate = entity.ToDate;
            IsActive = entity.IsActive;
            Description = entity.Description;
            return this;
        }

        public override RequestAgent Create() =>
            new RequestAgent
            {
                RequestId = RequestId,
                AgentId = AgentId,
                FromDate = FromDate,
                ToDate = ToDate,
                IsActive = IsActive,
                Description = Description,
            };

        public override RequestAgent Update() =>
            new RequestAgent
            {
                RequestId = RequestId,
                AgentId = AgentId,
                FromDate = FromDate,
                ToDate = ToDate,
                IsActive = IsActive,
                Description = Description,
                Id = Id,
            };
    }
}
