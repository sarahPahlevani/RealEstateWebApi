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
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Controllers.BasicInformation
{
    public class PropertyLabelController : ModelPagingController<PropertyLabel, PropertyLabelDto, PropertyLabelDto>
    {
        private readonly ILanguageProvider _languageProvider;

        private Func<IQueryable<PropertyLabel>, IQueryable<PropertyLabelDto>> _converter
            => items => items.Select(i => new PropertyLabelDto
            {
                Name = i.Name,
                Id = i.Id
            });

        public PropertyLabelController(IModelService<PropertyLabel, PropertyLabelDto> modelService,
            ILanguageProvider languageProvider) : base(modelService)
        {
            _languageProvider = languageProvider;
        }

        public override Func<IQueryable<PropertyLabel>, IQueryable<PropertyLabelDto>> DtoConverter => _converter;
        public override Func<IQueryable<PropertyLabel>, IQueryable<PropertyLabelDto>> PagingConverter => _converter;

        [AllowAnonymous]
        public override Task<ActionResult<IEnumerable<PropertyLabelDto>>> GetAllAsync(CancellationToken cancellationToken) 
            => base.GetAllAsync(cancellationToken);

        [AllowAnonymous]
        [HttpGet("[Action]/{language}")]
        public async Task<ActionResult<IEnumerable<PropertyLabelDto>>> GetAllByLanguage(string language,
            CancellationToken cancellationToken)
        {
            try
            {
                var lang = _languageProvider[language];
                return await DtoConverter(ModelService.DbContext.PropertyLabel.Translate(lang.Id)).ToListAsync(cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                return await base.GetAllAsync(cancellationToken);
            }
        }
    }
}
