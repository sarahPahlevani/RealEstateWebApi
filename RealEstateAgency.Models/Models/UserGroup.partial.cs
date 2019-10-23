using RealEstateAgency.DAL.Contracts;
using System.Linq;

namespace RealEstateAgency.DAL.Models
{
    public partial class UserGroup : IEntity, ITranslatable<UserGroup>, ICacheResult, IForbiddenModify
    {
        public UserGroup Translate(int languageId)
        {
            return new UserGroup
            {
                Id = Id,
                Name = UserGroupTranslate.Any(t => t.UserGroupId == Id && t.LanguageId == languageId)
                    ? UserGroupTranslate.First().Name
                    : Name
            };
        }
    }
}
