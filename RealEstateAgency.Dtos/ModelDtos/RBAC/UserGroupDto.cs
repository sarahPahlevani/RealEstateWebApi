using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class UserGroupDto : ModelDtoBase<UserGroup>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public string StaticCode { get; set; }

        public override IModelDto<UserGroup> From(UserGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            StaticCode = entity.StaticCode;
            return this;
        }

        public override UserGroup Create() =>
            new UserGroup
            {
                Name = Name,
                StaticCode = StaticCode
            };

        public override UserGroup Update() =>
            new UserGroup
            {
                Name = Name,
                StaticCode = StaticCode,
                Id = Id
            };
    }
}
