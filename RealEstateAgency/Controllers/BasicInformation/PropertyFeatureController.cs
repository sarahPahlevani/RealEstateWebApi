using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.BasicInformation;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.Extensions;
using RealEstateAgency.Implementations.Providers;

namespace RealEstateAgency.Controllers.BasicInformation
{
    public class PropertyFeatureController : ModelPagingController<PropertyFeature, PropertyFeatureDto, PropertyFeatureDto>
    {
        private readonly ILanguageProvider _languageProvider;

        public PropertyFeatureController(IModelService<PropertyFeature, PropertyFeatureDto> modelService,
            ILanguageProvider languageProvider)
            : base(modelService)
        {
            _languageProvider = languageProvider;
        }

        private Func<IQueryable<PropertyFeature>, IQueryable<PropertyFeatureDto>> _converter
         => entities => entities.Select(i => new PropertyFeatureDto
         {
             Name = i.Name,
             Id = i.Id
         });

        [HttpPatch("[Action]")]
        public async Task<ActionResult> SetPropertyFeatures([FromBody] SetPropertyFeaturesDto value,
            CancellationToken cancellationToken)
        {
            ModelService.DbContext.PropertyInvolveFeature.RemoveRange(await ModelService.DbContext.PropertyInvolveFeature
                .Where(i => i.PropertyId == value.PropertyId).ToListAsync(cancellationToken));
            await ModelService.DbContext.SaveChangesAsync(cancellationToken);
            await ModelService.DbContext.PropertyInvolveFeature.AddRangeAsync(
                value.PropertyFeatureIds.Select(i => new PropertyInvolveFeature
                {
                    PropertyFeatureId = i,
                    PropertyId = value.PropertyId
                }), cancellationToken);
            await ModelService.DbContext.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpGet("[Action]/{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyFeatureDto>>> GetPropertyFeatures(int propertyId,
            CancellationToken cancellationToken) =>
            await ModelService.DbContext.PropertyInvolveFeature.Include(i => i.Property)
                .Include(i => i.PropertyFeature).Where(i => i.Property.Id == propertyId)
                .Select(i => new PropertyFeatureDto
                {
                    Id = i.PropertyFeatureId,
                    Name = i.PropertyFeature.Name
                }).ToListAsync(cancellationToken);

        public override Func<IQueryable<PropertyFeature>, IQueryable<PropertyFeatureDto>> DtoConverter => _converter;
        public override Func<IQueryable<PropertyFeature>, IQueryable<PropertyFeatureDto>> PagingConverter => _converter;

        [AllowAnonymous]
        public override Task<ActionResult<IEnumerable<PropertyFeatureDto>>> GetAllAsync(CancellationToken cancellationToken) 
            => base.GetAllAsync(cancellationToken);

        [AllowAnonymous]
        [HttpGet("[Action]/{language}")]
        public async Task<ActionResult<IEnumerable<PropertyFeatureDto>>> GetAllByLanguage(string language,
            CancellationToken cancellationToken)
        {
            try
            {
                var lang = _languageProvider[language];
                return await DtoConverter(ModelService.DbContext.PropertyFeature.Translate(lang.Id)).ToListAsync(cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                return await base.GetAllAsync(cancellationToken);
            }
        }
    }
}
