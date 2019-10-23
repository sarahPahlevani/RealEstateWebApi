using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class UserGroupTranslateDto : ModelDtoBase<UserGroupTranslate>
    {
        public override int Id { get; set; }
        public int UserGroupId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }

        public override IModelDto<UserGroupTranslate> From(UserGroupTranslate entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            LanguageId = entity.LanguageId;
            UserGroupId = entity.UserGroupId;
            return this;
        }

        public override UserGroupTranslate Create() =>
            new UserGroupTranslate
            {
                Name = Name,
                LanguageId = LanguageId,
                UserGroupId = UserGroupId
            };

        public override UserGroupTranslate Update() =>
            new UserGroupTranslate
            {
                Id = Id,
                Name = Name,
                LanguageId = LanguageId,
                UserGroupId = UserGroupId
            };
    }
}
