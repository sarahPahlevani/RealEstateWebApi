using System;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.Authentication.Contracts;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Extensions;

namespace RealEstateAgency.Implementations.Authentication
{
    public class UserAccessService<TEntity> : IUserAccessService<TEntity>
        where TEntity : class, IEntity
    {
        private readonly string _userRole;
        public UserAccessService(IUserProvider userProvider)
        {
            _userRole = userProvider.Role;
            CheckForModifyAttribute();
            if (HasAttribute<DeleteAccessAttribute>())
                DeleteAccess = CheckAndSetAccess<DeleteAccessAttribute>();
            if (HasAttribute<CreateAccessAttribute>())
                CreateAccess = CheckAndSetAccess<CreateAccessAttribute>();
            if (HasAttribute<UpdateAccessAttribute>())
                UpdateAccess = CheckAndSetAccess<UpdateAccessAttribute>();
            GetAccess = CheckAndSetAccess<GetAccessAttribute>();
            ModifyAccess = CreateAccess && DeleteAccess && UpdateAccess;
        }

        private void CheckForModifyAttribute()
        {
            var access = CheckAndSetAccess<ModifyAccessAttribute>();
            DeleteAccess = access;
            UpdateAccess = access;
            CreateAccess = access;
        }
         
        private bool CheckAndSetAccess<TAccessAttribute>()
        where TAccessAttribute : class, IUserAccessAttribute
        {
            var roles = typeof(TEntity).GetAccessAttributeRoles<TAccessAttribute>();
            return roles is null || roles.Contains(_userRole);
        }

        private bool HasAttribute<TAccessAttribute>()
            where TAccessAttribute : Attribute =>
            typeof(TEntity).HasAttribute<TAccessAttribute>();

        public bool DeleteAccess { get; private set; }
        public bool CreateAccess { get; private set; }
        public bool UpdateAccess { get; private set; }
        public bool GetAccess { get; }
        public bool ModifyAccess { get; }
    }
}
