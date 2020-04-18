using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.Collections.Generic;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class UserGroupPermissionDto : ModelDtoBase<UserGroupPermission>
    {

        public override int Id { get; set; }

        public int UserGroupId { get; set; }
        public int MenuId { get; set; }
        public bool DeletePermission { get; set; }
        public bool UpdatePermission { get; set; }
        public bool ReadPermission { get; set; }
        public override UserGroupPermission Create() => new UserGroupPermission
        {
            Id = Id,
            UserGroupId = UserGroupId,
            MenuId = MenuId,
            DeletePermission = DeletePermission,
            UpdatePermission = UpdatePermission,
            ReadPermission = ReadPermission
        };
        public override IModelDto<UserGroupPermission> From(UserGroupPermission entity)
        {

            Id = Id;
            UserGroupId = UserGroupId;
            MenuId = MenuId;
            DeletePermission = DeletePermission;
            UpdatePermission = UpdatePermission;
            ReadPermission = ReadPermission;

            return this;
        }

        public override UserGroupPermission Update() => new UserGroupPermission
        {
            Id = Id,
            UserGroupId = UserGroupId,
            MenuId = MenuId,
            DeletePermission = DeletePermission,
            UpdatePermission = UpdatePermission,
            ReadPermission = ReadPermission
        };

    }
    public class UserPermission ///: ModelDtoBase<UserGroupPermission>
    {

        //  public override int Id { get; set; }
        public int Id { get; set; }
        public string Menu { get; set; }
        public int MenuId { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public bool HasUpdatePermmite { get; set; }
        public bool HasDeletePermmite { get; set; }
        public bool HasReadPermmite { get; set; }

        
    }
    public class UserPermissionList:IDto
    {
        public int Id { get; set; }
        public string Menu { get; set; }
        public int MenuId { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public bool HasUpdatePermmite { get; set; }
        public bool HasDeletePermmite { get; set; }
        public bool HasReadPermmite { get; set; }

    }

}
