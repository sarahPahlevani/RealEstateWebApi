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

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyPriceController : ModelController<PropertyPrice, PropertyPriceDto>
    {
        private readonly IEntityService<Property> _propertyService;

        public PropertyPriceController(IModelService<PropertyPrice, PropertyPriceDto> modelService, IEntityService<Property> propertyService) : base(modelService)
        {
            _propertyService = propertyService;
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
            var unit = new RealEstateDbContext().PriceScaleUnit.FirstOrDefault(r => r.Id == value.PriceScaleUnitId);
            if (unit is null)
                throw new Exception("not found price unit");

            value.CalculatedPriceUnit = value.Price * unit.Scale;
            var res = await ModelService.CreateByDtoAsync(value, cancellationToken);

            return res;
        }

        [HttpPut]
        public override async Task<ActionResult> UpdateAsync(PropertyPriceDto value, CancellationToken cancellationToken)
        {
            var unit = new RealEstateDbContext().PriceScaleUnit.FirstOrDefault(r => r.Id == value.PriceScaleUnitId);
            if (unit is null)
                throw new Exception("not found price unit");

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
        public ActionResult<PriceMinMax> GetMinMax()
        {
            var min = ModelService.Queryable.Min(r => (decimal?)r.CalculatedPriceUnit).GetValueOrDefault(0);
            var max = ModelService.Queryable.Max(r => (decimal?)r.CalculatedPriceUnit).GetValueOrDefault(0);

            return new PriceMinMax
            {
                Min = min,
                Max = max,
            };
        }

    }
}
