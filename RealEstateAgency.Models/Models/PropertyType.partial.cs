using RealEstateAgency.DAL.Contracts;
using System.Linq;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{
    [ModifyAccess(UserGroups.Administrator, UserGroups.RealEstateAdministrator)]
    public partial class PropertyType : IVirtualDelete, ITranslatable<PropertyType>
    {
        public PropertyType Translate(int languageId)
        {
            return new PropertyType
            {
                Id = Id,
                Name = PropertyTypeTranslate.Any(t => t.PropertyTypeId == Id && t.LanguageId == languageId)
                    ? PropertyTypeTranslate.First().Name
                    : Name,
                Icon = Icon,
            };
        }
    }
}
