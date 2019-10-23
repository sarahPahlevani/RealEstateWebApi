using RealEstateAgency.DAL.Contracts;
using System.Linq;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{
    [ModifyAccess(UserGroups.Administrator,UserGroups.RealEstateAdministrator)]
    public partial class ActionType : IEntity, ITranslatable<ActionType>, ICacheResult
    {
        public ActionType Translate(int languageId)
        {
            return new ActionType
            {
                Id = Id,
                Name = ActionTypeTranslate.Any(t => t.ActionTypeId == Id && t.LanguageId == languageId)
                    ? ActionTypeTranslate.First().Name
                    : Name
            };
        }
    }
}
