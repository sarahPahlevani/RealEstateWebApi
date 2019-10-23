using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.BasicInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Implementations.Extensions;
using RealEstateAgency.Implementations.Providers;

namespace RealEstateAgency.Controllers.BasicInformation
{
    public class PropertyTypeController : ModelPagingController<PropertyType, PropertyTypeDto, PropertyTypeDto>
    {
        private readonly ILanguageProvider _languageProvider;

        public PropertyTypeController(IModelService<PropertyType, PropertyTypeDto> modelService
        , ILanguageProvider languageProvider)
            : base(modelService)
        {
            _languageProvider = languageProvider;
        }

        private Func<IQueryable<PropertyType>, IQueryable<PropertyTypeDto>> _converter
            => entities => entities.Select(i => new PropertyTypeDto
            {
                Name = i.Name,
                Id = i.Id,
            });

        public override Func<IQueryable<PropertyType>, IQueryable<PropertyTypeDto>> DtoConverter => _converter;
        public override Func<IQueryable<PropertyType>, IQueryable<PropertyTypeDto>> PagingConverter => _converter;

        [AllowAnonymous]
        public override Task<ActionResult<IEnumerable<PropertyTypeDto>>> GetAllAsync(CancellationToken cancellationToken)
            => base.GetAllAsync(cancellationToken);

        [AllowAnonymous]
        [HttpGet("[Action]/{language}")]
        public async Task<ActionResult<IEnumerable<PropertyTypeDto>>> GetAllByLanguage(string language,
            CancellationToken cancellationToken)
        {
            try
            {
                var lang = _languageProvider[language];
                return await DtoConverter(ModelService.DbContext.PropertyType.Translate(lang.Id)).ToListAsync(cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                return await base.GetAllAsync(cancellationToken);
            }
        }

    }
}
