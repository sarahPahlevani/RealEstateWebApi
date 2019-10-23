using RealEstateAgency.DAL.Contracts;
using System.Linq;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{
    [ModifyAccess(UserGroups.Administrator, UserGroups.RealEstateAdministrator)]
    public partial class PropertyLabel : IVirtualDelete, ITranslatable<PropertyLabel>
    {
        public PropertyLabel Translate(int languageId)
        {
            return new PropertyLabel
            {
                Id = Id,
                Name = PropertyLabelTranslate.Any(t => t.PropertyLabelId == Id && t.LanguageId == languageId)
                    ? PropertyLabelTranslate.First().Name
                    : Name,
            };
        }
    }
}
