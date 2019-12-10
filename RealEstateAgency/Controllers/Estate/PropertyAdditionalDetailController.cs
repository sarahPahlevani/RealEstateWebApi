using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateAgency.Controllers.Estate
{
    public class PropertyAdditionalDetailController : ModelController<PropertyAdditionalDetail, PropertyAdditionalDetailDto>
    {
        private readonly IEntityService<Property> _propertyService;

        public PropertyAdditionalDetailController(IModelService<PropertyAdditionalDetail, PropertyAdditionalDetailDto> modelService
            , IEntityService<Property> propertyService) : base(modelService)
        {
            _propertyService = propertyService;
        }

        [HttpGet("[Action]/{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyAdditionalDetailDto>>> GetPropertyAdditionalDetails(int propertyId, CancellationToken cancellationToken)
            => await ModelService.DataConvertQuery(ModelService.AsQueryable(i => i.PropertyId == propertyId)).ToListAsync(cancellationToken);

        public override Func<IQueryable<PropertyAdditionalDetail>, IQueryable<PropertyAdditionalDetailDto>> DtoConverter
        => items => items.Select(i => new PropertyAdditionalDetailDto
        {
            PropertyId = i.PropertyId,
            Value = i.Value,
            Title = i.Title,
            Id = i.Id
        });

        [HttpPost]
        public override async Task<ActionResult<PropertyAdditionalDetailDto>> Create(PropertyAdditionalDetailDto value, CancellationToken cancellationToken)
        {
            var res = await ModelService.CreateByDtoAsync(value, cancellationToken);

            var property = await _propertyService.GetAsync(value.PropertyId, cancellationToken);
            if (property != null && property.IsPublished)
            {
                property.IsPublished = false;
                await _propertyService.UpdateAsync(property, cancellationToken);
            }

            return res;
        }

        [HttpPut]
        public override async Task<ActionResult> UpdateAsync(PropertyAdditionalDetailDto value, CancellationToken cancellationToken)
        {
            await ModelService.UpdateByDtoAsync(value, cancellationToken);

            var property = await _propertyService.GetAsync(value.PropertyId, cancellationToken);
            if (property != null && property.IsPublished)
            {
                property.IsPublished = false;
                await _propertyService.UpdateAsync(property, cancellationToken);
            }

            return NoContent();
        }

    }
}
