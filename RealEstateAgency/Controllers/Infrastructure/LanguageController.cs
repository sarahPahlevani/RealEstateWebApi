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
using RealEstateAgency.Implementations.Providers;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.Controllers.Infrastructure
{
    [AllowAnonymous]
    public class LanguageController : ModelController<Language, LanguageDto>
    {
        private readonly ILanguageProvider _languageProvider;

        public LanguageController(IModelService<Language, LanguageDto> modelService,
            ILanguageProvider languageProvider) : base(modelService)
        {
            _languageProvider = languageProvider;
        }

        public override Func<IQueryable<Language>, IQueryable<LanguageDto>> DtoConverter =>
            items => items.Select(i => new LanguageDto
            {
                Id = i.Id,
                IsDefault = i.IsDefault,
                Code = i.Code,
                Type = i.Type,
                UrlCode = i.Code.Substring(0, 2).ToLower()
            });

        
        public override Task<ActionResult<IEnumerable<LanguageDto>>> GetAllAsync(CancellationToken cancellationToken)
            => base.GetAllAsync(cancellationToken);

        //[Authorize(Roles = UserGroups.RealEstateAdministrator)]
        [HttpPut("[Action]")]
        public ActionResult SetRealEstateLanguage([FromBody] LanguageDto languageDto, CancellationToken CancellationToken)
        {
            var lang = ModelService.Get(languageDto.Id);
            var realEstate = ModelService.DbContext.RealEstate.First();
            realEstate.LanguageIdDefault = lang.Id;
            ModelService.DbContext.RealEstate.Update(realEstate);
            ModelService.DbContext.SaveChanges();
            _languageProvider.ChangeLanguage((LanguageDto)new LanguageDto().From(lang));
            return NoContent();
        }
    }
}
