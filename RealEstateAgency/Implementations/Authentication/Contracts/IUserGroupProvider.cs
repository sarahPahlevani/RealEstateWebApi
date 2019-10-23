using System.Collections.Generic;
using RealEstateAgency.Dtos.Other.UserGroup;

namespace RealEstateAgency.Implementations.Authentication.Contracts
{
    public interface IUserGroupProvider
    {
        Dictionary<UserGroup, UserGroupMetadata> Groups { get; }
        UserGroupMetadata this[UserGroup group] { get; }
        UserGroupMetadata this[string group] { get; }
    }
}
