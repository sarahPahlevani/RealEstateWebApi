using RealEstateAgency.DAL.Contracts;
using System.Linq;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{
    [ModifyAccess(UserGroups.Administrator, UserGroups.RealEstateAdministrator)]
    public partial class ImportantPlaceType : IVirtualDelete, ITranslatable<ImportantPlaceType>
    {
        public ImportantPlaceType Translate(int languageId)
        {
            return new ImportantPlaceType
            {
                Id = Id,
                Name = ImportantPlaceTypeTranslate.Any(t => t.ImportantPlaceTypeId == Id && t.LanguageId == languageId)
                    ? ImportantPlaceTypeTranslate.First().Name
                    : Name,
                TypeIcon = TypeIcon,
                Deleted = Deleted,
                DeletedDate = DeletedDate,
                UserAccountIdDeleteBy = UserAccountIdDeleteBy
            };
        }
    }
}
