using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;

namespace RealEstateAgency.Implementations.Providers
{
    public class LanguageProvider : ILanguageProvider
    {
        public LanguageProvider(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<RealEstateDbContext>();
                var languages = context.Language.ToList();
                var realEstate = context.RealEstate.Include(e => e.LanguageIdDefaultNavigation).First();
                Languages = new Dictionary<string, LanguageDto>();
                languages.ForEach(l => Languages.Add(l.Code.Split("-")[0].ToLower(), (LanguageDto)(new LanguageDto()).From(l)));
                SelectedLanguage = (LanguageDto)(new LanguageDto()).From(realEstate.LanguageIdDefaultNavigation);
            }
        }
        public Dictionary<string, LanguageDto> Languages { get; }
        public LanguageDto SelectedLanguage { get; private set; }

        public LanguageDto this[string lang] => Languages[lang];
        public void ChangeLanguage(LanguageDto languageDto) 
            => SelectedLanguage = languageDto;
    }

    public interface ILanguageProvider
    {
        Dictionary<string, LanguageDto> Languages { get; }
        LanguageDto SelectedLanguage { get; }
        LanguageDto this[string lang] { get; }
        void ChangeLanguage(LanguageDto languageDto);
    }
}
