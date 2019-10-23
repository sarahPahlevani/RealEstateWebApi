using RealEstateAgency.DAL.Contracts;
using System.Linq;

namespace RealEstateAgency.DAL.Models
{
    public partial class Region : IEntity, IForbiddenModify, ITranslatable<Region>, ICacheResult
    {
        public Region Translate(int languageId)
        {
            return new Region
            {
                Id = Id,
                Name = RegionTranslate.Any(t => t.RegionId == Id && t.LanguageId == languageId)
                    ? RegionTranslate.First().Name
                    : Name
            };
        }
    }
}
