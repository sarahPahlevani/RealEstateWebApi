using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Organization
{
    public class AgentDto : ModelDtoBase<Agent>
    {
        public override int Id { get; set; }

        [Required]
        public int RealEstateId { get; set; }

        [Required]
        public int UserAccountId { get; set; }

        public string MetadataJson { get; set; }
        public byte[] BusinessCardFront { get; set; }
        public byte[] BusinessCardBack { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsResponsible { get; set; }
        [Required]
        public bool HasPublishingAuthorization { get; set; }

        public override IModelDto<Agent> From(Agent entity)
        {
            Id = entity.Id;
            RealEstateId = entity.RealEstateId;
            UserAccountId = entity.UserAccountId;
            MetadataJson = entity.MetadataJson;
            BusinessCardFront = entity.BusinessCardFront;
            BusinessCardBack = entity.BusinessCardBack;
            Description = entity.Description;
            IsResponsible = entity.IsResponsible;
            HasPublishingAuthorization = entity.HasPublishingAuthorization;
            return this;
        }

        public override Agent Create() =>
            new Agent
            {
                RealEstateId = RealEstateId,
                UserAccountId = UserAccountId,
                MetadataJson = MetadataJson,
                BusinessCardFront = BusinessCardFront,
                BusinessCardBack = BusinessCardBack,
                Description = Description,
                IsResponsible = IsResponsible,
                HasPublishingAuthorization = HasPublishingAuthorization
            };

        public override Agent Update() =>
            new Agent
            {
                Id = Id,
                RealEstateId = RealEstateId,
                UserAccountId = UserAccountId,
                MetadataJson = MetadataJson,
                BusinessCardFront = BusinessCardFront,
                BusinessCardBack = BusinessCardBack,
                Description = Description,
                IsResponsible = IsResponsible,
                HasPublishingAuthorization = HasPublishingAuthorization
            };
    }
}
