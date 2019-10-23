using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
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

namespace RealEstateAgency.Controllers.Infrastructure
{
    public class RegionController : ModelController<Region, RegionDto>
    {
        private readonly ILanguageProvider _languageProvider;

        public RegionController(IModelService<Region, RegionDto> modelService,ILanguageProvider languageProvider) : base(modelService)
        {
            _languageProvider = languageProvider;
        }

        public override Func<IQueryable<Region>, IQueryable<RegionDto>> DtoConverter
        => items => items.Select(i => new RegionDto
        {
            Id = i.Id,
            Name = i.Name,
            CountryId = i.CountryId
        });

        [AllowAnonymous]
        [HttpGet("[Action]/{language}")]
        public async Task<ActionResult<IEnumerable<RegionDto>>> GetAllByLanguage(string language,
            CancellationToken cancellationToken)
        {
            try
            {
                var lang = _languageProvider[language];
                return await DtoConverter(ModelService.DbContext.Region.Translate(lang.Id)).ToListAsync(cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                return await base.GetAllAsync(cancellationToken);
            }
        }
    }
}
