using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Infrastructure
{
    public class PriceScaleController : ModelPagingController<PriceScaleUnit, PriceScaleUnitDto, PriceScaleUnitDto>
    {
        public PriceScaleController(IModelService<PriceScaleUnit, PriceScaleUnitDto> modelService) : base(modelService)
        {
        }

        private Func<IQueryable<PriceScaleUnit>, IQueryable<PriceScaleUnitDto>> _converter
        => items => items.Select(i => new PriceScaleUnitDto
        {
            Id = i.Id,
            Name = i.Name,
            Scale = i.Scale
        });

        public override Func<IQueryable<PriceScaleUnit>, IQueryable<PriceScaleUnitDto>> DtoConverter => _converter;
        public override Func<IQueryable<PriceScaleUnit>, IQueryable<PriceScaleUnitDto>> PagingConverter => _converter;
    }
}
