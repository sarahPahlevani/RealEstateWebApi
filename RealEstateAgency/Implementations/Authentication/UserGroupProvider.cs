using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.Other.UserGroup;
using RealEstateAgency.Implementations.Authentication.Contracts;
using RealEstateAgency.Shared.Statics;
using UserGroup = RealEstateAgency.Dtos.Other.UserGroup.UserGroup;
using UserGroupModel = RealEstateAgency.DAL.Models.UserGroup;

namespace RealEstateAgency.Implementations.Authentication
{
    public class UserGroupProvider : IUserGroupProvider
    {
        private readonly List<UserGroupModel> _groups;
        public UserGroupProvider(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<RealEstateDbContext>();
                _groups = context.UserGroup.ToList();
            }
        }

        public Dictionary<UserGroup, UserGroupMetadata> Groups => GroupsLazy.Value;

        public Lazy<Dictionary<UserGroup, UserGroupMetadata>> GroupsLazy
        => new Lazy<Dictionary<UserGroup, UserGroupMetadata>>(LoadGroupModels);

        public UserGroupMetadata this[UserGroup name] => Groups[name];
        public UserGroupMetadata this[string name] => Groups[Map(name)];

        private Dictionary<UserGroup, UserGroupMetadata> LoadGroupModels()
        {
            var modelsList = new Dictionary<UserGroup, UserGroupMetadata>();

            _groups.ForEach(g =>
            {
                var groupType = Map(g.StaticCode);
                modelsList.Add(groupType, UserGroupMetadata.Create(groupType,g));
            });
            return modelsList;
        }

        public static UserGroup Map(string userGroupName)
        {
            userGroupName = userGroupName.Trim();
            switch (userGroupName)
            {
                case UserGroups.Administrator: return UserGroup.Administrator;
                case UserGroups.RealEstateAdministrator: return UserGroup.RealEstateAdministrator;
                case UserGroups.Agent: return UserGroup.Agent;
                case UserGroups.MarketAssistance: return UserGroup.MarketAssistance;
                case UserGroups.MarketAssistancePlus: return UserGroup.MarketAssistancePlus;
                case UserGroups.PropertyOwner: return UserGroup.PropertyOwner;
                case UserGroups.AppClient: return UserGroup.AppClient;
                default: return UserGroup.AppClient;
            }
        }
    }
}
