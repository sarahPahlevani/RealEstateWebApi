using RealEstateAgency.DAL.Contracts;
using System.Linq;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{
    [ModifyAccess(UserGroups.Administrator, UserGroups.RealEstateAdministrator)]
    public partial class PropertyFeature : IVirtualDelete, ITranslatable<PropertyFeature>
    {
        public PropertyFeature Translate(int languageId)
        {
            return new PropertyFeature
            {
                Id = Id,
                Name = PropertyFeatureTranslate.Any(t => t.PropertyFeatureId == Id && t.LanguageId == languageId)
                    ? PropertyFeatureTranslate.First().Name
                    : Name
            };
        }
    }
}
