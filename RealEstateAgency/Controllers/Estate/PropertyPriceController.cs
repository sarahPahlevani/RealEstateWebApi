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
using RealEstateAgency.Implementations.ApiImplementations.Models;
using System.Collections.Generic;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyPriceController : ModelController<PropertyPrice, PropertyPriceDto>
    {
        private readonly IEntityService<Property> _propertyService;
        private readonly IEntityService<PriceScaleUnit> _priceScaleUnitService;
        private readonly IEntityService<Currency> _currencyService;

        public PropertyPriceController(IModelService<PropertyPrice, PropertyPriceDto> modelService, IEntityService<Property> propertyService,
            IEntityService<PriceScaleUnit> priceScaleUnitService, IEntityService<Currency> currencyService) : base(modelService)
        {
            _priceScaleUnitService = priceScaleUnitService;
            _propertyService = propertyService;
            _currencyService = currencyService;
        }

        public override Func<IQueryable<PropertyPrice>, IQueryable<PropertyPriceDto>> DtoConverter
        => items => items.Select(i => new PropertyPriceDto
        {
            Id = i.Id,
            PropertyId = i.Id,
            CurrencyId = i.CurrencyId,
            PriceScaleUnitId = i.PriceScaleUnitId,
            Price = i.Price,
            BeforePriceLabel = i.BeforePriceLabel,
            AfterPriceLabel = i.AfterPriceLabel,
            PriceOnCall = i.PriceOnCall,
        });

        [HttpPost]
        public override async Task<ActionResult<PropertyPriceDto>> Create(PropertyPriceDto value, CancellationToken cancellationToken)
        {
            var unit = await _priceScaleUnitService.GetAsync(value.PriceScaleUnitId, cancellationToken);
            if (unit is null)
                throw new Exception("not found price scale unit");

            value.CalculatedPriceUnit = value.Price * unit.Scale;
            var res = await ModelService.CreateByDtoAsync(value, cancellationToken);

            return res;
        }

        [HttpPut]
        public override async Task<ActionResult> UpdateAsync(PropertyPriceDto value, CancellationToken cancellationToken)
        {
            var unit = await _priceScaleUnitService.GetAsync(value.PriceScaleUnitId, cancellationToken);
            if (unit is null)
                throw new Exception("not found price scale unit");

            value.CalculatedPriceUnit = value.Price * unit.Scale;
            await ModelService.UpdateByDtoAsync(value, cancellationToken);

            var property = await _propertyService.GetAsync(value.PropertyId, cancellationToken);
            if (property != null && property.IsPublished)
            {
                property.IsPublished = false;
                await _propertyService.UpdateAsync(property, cancellationToken);
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("[Action]")]
        public ActionResult<PriceMinMax> GetMinMax(/*int? currencyId*/)
        {
            var min = ModelService.Queryable.Where(r => r.IdNavigation.IsPublished /*&& (!currencyId.HasValue || r.CurrencyId == currencyId.Value)*/)
                .Min(r => (decimal?)r.CalculatedPriceUnit).GetValueOrDefault(0);
            var max = ModelService.Queryable.Where(r => r.IdNavigation.IsPublished /*&& (!currencyId.HasValue || r.CurrencyId == currencyId.Value)*/)
                .Max(r => (decimal?)r.CalculatedPriceUnit).GetValueOrDefault(0);

            return new PriceMinMax
            {
                Min = min,
                Max = max,
            };
        }


        [AllowAnonymous]
        [HttpGet("[Action]")]
        public ActionResult<List<Currency>> GetActiveCurrency()
        {
            var list= ModelService.Queryable
                .Include(r => r.Currency)
                .Where(r => r.IdNavigation.IsPublished)
                .GroupBy(r => r.CurrencyId)
                .Select(r => r.Key).ToList();

            return _currencyService.AsQueryable(r => list.Contains(r.Id)).ToList();
        }

    }
}
