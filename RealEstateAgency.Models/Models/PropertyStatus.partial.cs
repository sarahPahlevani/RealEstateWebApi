using RealEstateAgency.DAL.Contracts;
using System.Linq;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{
    [ModifyAccess(UserGroups.Administrator, UserGroups.RealEstateAdministrator)]
    public partial class PropertyStatus : IVirtualDelete, ITranslatable<PropertyStatus>
    {
        public PropertyStatus Translate(int languageId)
        {
            return new PropertyStatus
            {
                Id = Id,
                Name = PropertyStatusTranslate.Any(t => t.PropertyStatusId == Id && t.LanguageId == languageId)
                    ? PropertyStatusTranslate.First().Name
                    : Name
            };
        }
    }
}
