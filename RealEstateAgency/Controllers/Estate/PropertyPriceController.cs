using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyPriceController : ModelController<PropertyPrice, PropertyPriceDto>
    {
        public PropertyPriceController(IModelService<PropertyPrice, PropertyPriceDto> modelService) : base(modelService)
        { }

        public override Func<IQueryable<PropertyPrice>, IQueryable<PropertyPriceDto>> DtoConverter
        => items => items.Select(i => new PropertyPriceDto
        {
            PropertyId = i.Id,
            Id = i.Id,
            CurrencyId = i.CurrencyId,
            PriceScaleUnitId = i.PriceScaleUnitId,
            Price = i.Price,
            BeforePriceLabel = i.BeforePriceLabel,
            AfterPriceLabel = i.AfterPriceLabel,
            PriceOnCall = i.PriceOnCall,
        });
    }
}
