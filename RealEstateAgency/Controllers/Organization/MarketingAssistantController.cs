using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Organization;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Organization
{
    public class MarketingAssistantController : ModelPagingController<MarketingAssistant, MarketingAssistantDto, MarketingAssistantDto>
    {
        public MarketingAssistantController(IModelService<MarketingAssistant, MarketingAssistantDto> modelService) : base(modelService)
        {
        }

        private Func<IQueryable<MarketingAssistant>, IQueryable<MarketingAssistantDto>> _converter
         => items => items.Select(i => new MarketingAssistantDto
         {
             RealEstateId = i.RealEstateId,
             UserAccountId = i.UserAccountId,
             MetadataJson = i.MetadataJson,
             Description = i.Description,
             TrackingCode = i.TrackingCode,
             HasPlusLevel = i.HasPlusLevel,
             Id = i.Id
         });

        public override Func<IQueryable<MarketingAssistant>, IQueryable<MarketingAssistantDto>> DtoConverter =>
            _converter;

        public override Func<IQueryable<MarketingAssistant>, IQueryable<MarketingAssistantDto>> PagingConverter =>
            _converter;
    }
}
