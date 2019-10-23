using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Infrastructure
{
    public class RegionTranslateController : ModelController<RegionTranslate, RegionTranslateDto>
    {
        public RegionTranslateController(IModelService<RegionTranslate, RegionTranslateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<RegionTranslate>, IQueryable<RegionTranslateDto>> DtoConverter
        => items => items.Select(i => new RegionTranslateDto
        {
            Id = i.Id,
            Name = i.Name,
            LanguageId = i.LanguageId,
            RegionId = i.RegionId
        });
    }
}
