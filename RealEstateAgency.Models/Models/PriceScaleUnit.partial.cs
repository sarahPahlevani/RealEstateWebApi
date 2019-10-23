using RealEstateAgency.DAL.Contracts;
using System.Linq;

namespace RealEstateAgency.DAL.Models
{
    public partial class PriceScaleUnit : IEntity, IForbiddenModify, ITranslatable<PriceScaleUnit>, ICacheResult
    {
        public PriceScaleUnit Translate(int languageId)
        {
            return new PriceScaleUnit
            {
                Id = Id,
                Name = PriceScaleUnitTranslate.Any(t => t.PriceScaleUnitId == Id && t.LanguageId == languageId)
                    ? PriceScaleUnitTranslate.First().Name
                    : Name,
            };
        }
    }
}
