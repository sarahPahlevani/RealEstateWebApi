using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;

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


        [AllowAnonymous]
        [HttpGet("GetPriceMinMax")]
        public ActionResult<PriceMinMaxDto> GetPriceMinMax()
        {
            var min = ModelService.Queryable.Min(r => r.CalculatedPriceUnit);
            var max = ModelService.Queryable.Max(r => r.CalculatedPriceUnit);
            return new PriceMinMaxDto
            {
                Min = min,
                Max = max,
            };
        }

    }
}
