using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.Organization
{
    public class MarketingAssistantDto : ModelDtoBase<MarketingAssistant>
    {
        public override int Id { get; set; }
        public int RealEstateId { get; set; }
        public int UserAccountId { get; set; }
        public string MetadataJson { get; set; }
        public string Description { get; set; }
        public string TrackingCode { get; set; }
        public bool HasPlusLevel { get; set; }

        public override IModelDto<MarketingAssistant> From(MarketingAssistant entity)
        {
            Id = entity.Id;
            RealEstateId = entity.RealEstateId;
            UserAccountId = entity.UserAccountId;
            MetadataJson = entity.MetadataJson;
            Description = entity.Description;
            TrackingCode = entity.TrackingCode;
            HasPlusLevel = entity.HasPlusLevel;
            return this;
        }

        public override MarketingAssistant Create()
            => new MarketingAssistant
            {
                RealEstateId = RealEstateId,
                UserAccountId = UserAccountId,
                MetadataJson = MetadataJson,
                Description = Description,
                TrackingCode = TrackingCode,
                HasPlusLevel = HasPlusLevel,
            };

        public override MarketingAssistant Update()
            => new MarketingAssistant
            {
                Id = Id,
                RealEstateId = RealEstateId,
                UserAccountId = UserAccountId,
                MetadataJson = MetadataJson,
                Description = Description,
                TrackingCode = TrackingCode,
                HasPlusLevel = HasPlusLevel,
            };
    }
}
