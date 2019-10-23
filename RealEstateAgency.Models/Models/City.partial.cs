using RealEstateAgency.DAL.Contracts;
using System.Linq;

namespace RealEstateAgency.DAL.Models
{
    public partial class City : IEntity, IForbiddenModify, ITranslatable<City>, ICacheResult
    {
        public City Translate(int languageId)
        {
            return new City
            {
                Id = Id,
                Name = CityTranslate.Any(t => t.CityId == Id && t.LanguageId == languageId)
                    ? CityTranslate.First().Name
                    : Name,
                RegionId = RegionId
            };
        }
    }
}
