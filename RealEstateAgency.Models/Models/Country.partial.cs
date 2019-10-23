using RealEstateAgency.DAL.Contracts;
using System.Linq;

namespace RealEstateAgency.DAL.Models
{
    public partial class Country : IEntity, IForbiddenModify, ITranslatable<Country>, ICacheResult
    {
        public Country Translate(int languageId)
        {
            return new Country
            {
                Id = Id,
                Name = CountryTranslate.Any(t => t.CountryId == Id && t.LanguageId == languageId)
                    ? CountryTranslate.First().Name
                    : Name,
                Isocode = Isocode,
                CurrencyId = CurrencyId,
                Isolong = Isolong,
                Isoshort = Isoshort,
                OfficialLongForm = OfficialLongForm,
                OfficialShortForm = OfficialShortForm
            };
        }
    }
}
