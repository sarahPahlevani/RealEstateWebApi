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
    public class PropertyStatusController : ModelPagingController<PropertyStatus, PropertyStatusDto, PropertyStatusDto>
    {
        private readonly ILanguageProvider _languageProvider;

        public PropertyStatusController(IModelService<PropertyStatus, PropertyStatusDto> modelService,
            ILanguageProvider languageProvider) : base(modelService)
        {
            _languageProvider = languageProvider;
        }

        private Func<IQueryable<PropertyStatus>, IQueryable<PropertyStatusDto>> _converter
        => items => items.Select(i => new PropertyStatusDto
        {
            Id = i.Id,
            Name = i.Name
        });

        public override Func<IQueryable<PropertyStatus>, IQueryable<PropertyStatusDto>> DtoConverter => _converter;
        public override Func<IQueryable<PropertyStatus>, IQueryable<PropertyStatusDto>> PagingConverter => _converter;

        [AllowAnonymous]
        public override Task<ActionResult<IEnumerable<PropertyStatusDto>>> GetAllAsync(CancellationToken cancellationToken) 
            => base.GetAllAsync(cancellationToken);

        [AllowAnonymous]
        [HttpGet("[Action]/{language}")]
        public async Task<ActionResult<IEnumerable<PropertyStatusDto>>> GetAllByLanguage(string language,
            CancellationToken cancellationToken)
        {
            try
            {
                var lang = _languageProvider[language];
                return await DtoConverter(ModelService.DbContext.PropertyStatus.Translate(lang.Id)).ToListAsync(cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                return await base.GetAllAsync(cancellationToken);
            }
        }
    }
}
