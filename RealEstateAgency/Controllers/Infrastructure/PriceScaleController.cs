using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Infrastructure
{
    public class PriceScaleController : ModelPagingController<PriceScaleUnit, PriceScaleUnitDto, PriceScaleUnitListDto>
    {
        public PriceScaleController(IModelService<PriceScaleUnit, PriceScaleUnitDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<PriceScaleUnit>, IQueryable<PriceScaleUnitDto>> DtoConverter =>
            items => items.Select(i => new PriceScaleUnitDto
            {
                Id = i.Id,
                Name = i.Name,
                Scale = i.Scale,
            });

        public override Func<IQueryable<PriceScaleUnit>, IQueryable<PriceScaleUnitListDto>> PagingConverter =>
            items => items.Select(i => new PriceScaleUnitListDto
            {
                Id = i.Id,
                Name = i.Name,
                Scale = i.Scale,
            });
    }
}
