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
using RealEstateAgency.Dtos.ModelDtos.BasicInformation;
using RealEstateAgency.Implementations.Extensions;
using RealEstateAgency.Implementations.Providers;

namespace RealEstateAgency.Controllers.Infrastructure
{
    public class CityController : ModelPagingController<City, CityDto, CityDto>
    {
        private readonly ILanguageProvider _languageProvider;

        public CityController(IModelService<City, CityDto> modelService, ILanguageProvider languageProvider) : base(modelService)
        {
            _languageProvider = languageProvider;
        }

        private readonly Func<IQueryable<City>, IQueryable<CityDto>> _baseConverter
            = items => items.Select(i => new CityDto
            {
                Id = i.Id,
                Name = i.Name,
                RegionId = i.RegionId
            });

        public override Func<IQueryable<City>, IQueryable<CityDto>> DtoConverter => _baseConverter;
        public override Func<IQueryable<City>, IQueryable<CityDto>> PagingConverter => _baseConverter;

        [AllowAnonymous]
        [HttpGet("[Action]/{language}")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetAllByLanguage(string language,
            CancellationToken cancellationToken)
        {
            try
            {
                var lang = _languageProvider[language];
                return await DtoConverter(ModelService.DbContext.City.Translate(lang.Id)).ToListAsync(cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                return await base.GetAllAsync(cancellationToken);
            }
        }
    }
}
