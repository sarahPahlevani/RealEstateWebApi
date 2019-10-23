using System;
using UserGroupModel = RealEstateAgency.DAL.Models.UserGroup;

namespace RealEstateAgency.Dtos.Other.UserGroup
{
    public abstract class UserGroupMetadata
    {
        public static UserGroupMetadata Create(UserGroup userGroupType, UserGroupModel userGroup)
        {
            switch (userGroupType)
            {
                case UserGroup.Administrator: return new AdministratorGroupModel(userGroup);
                case UserGroup.RealEstateAdministrator: return new RealEstateAdministratorGroupModel(userGroup);
                case UserGroup.Agent: return new AgentGroupModel(userGroup);
                case UserGroup.MarketAssistance: return new MarketAssistanceGroupModel(userGroup);
                case UserGroup.MarketAssistancePlus: return new MarketAssistancePlusGroupModel(userGroup);
                case UserGroup.PropertyOwner: return new PropertyOwnerGroupModel(userGroup);
                case UserGroup.AppClient: return new AppClientGroupModel(userGroup);
                default: throw new ArgumentOutOfRangeException(nameof(userGroupType), userGroupType, null);
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string StaticCode { get; set; }

        public abstract PageAccess DashboardAccess { get; set; }
        public abstract PageAccess EstateAccess { get; set; }
        public abstract PageAccess BasicInformationAccess { get; set; }
        public abstract PageAccess ProjectAccess { get; set; }
        public abstract PageAccess UsersAccess { get; set; }
        public abstract PageAccess AgentsAccess { get; set; }
    }

    public class AdministratorGroupModel : UserGroupMetadata
    {
        public AdministratorGroupModel(UserGroupModel userGroup)
        {
            Id = userGroup.Id;
            Name = userGroup.Name;
            StaticCode = userGroup.StaticCode;
        }

        public override PageAccess DashboardAccess { get; set; } = new PageAccess
        {
            Name = nameof(DashboardAccess)
        };
        public override PageAccess EstateAccess { get; set; } = new PageAccess
        {
            Name = nameof(EstateAccess)
        };
        public override PageAccess BasicInformationAccess { get; set; } = new PageAccess
        {
            Name = nameof(BasicInformationAccess)
        };

        public override PageAccess ProjectAccess { get; set; } = new PageAccess
        {
            Name = nameof(ProjectAccess),
        };

        public override PageAccess UsersAccess { get; set; } = new PageAccess()
        {
            Name = nameof(UsersAccess),
        };

        public override PageAccess AgentsAccess { get; set; } = new PageAccess()
        {
            Name = nameof(AgentsAccess),
        };
    }

    public class RealEstateAdministratorGroupModel : UserGroupMetadata
    {
        public RealEstateAdministratorGroupModel(UserGroupModel userGroup)
        {
            Id = userGroup.Id;
            Name = userGroup.Name;
            StaticCode = userGroup.StaticCode;
        }

        public override PageAccess DashboardAccess { get; set; } = new PageAccess
        {
            Name = nameof(DashboardAccess)
        };
        public override PageAccess EstateAccess { get; set; } = new PageAccess
        {
            Name = nameof(EstateAccess)
        };
        public override PageAccess BasicInformationAccess { get; set; } = new PageAccess
        {
            Name = nameof(BasicInformationAccess)
        };
        public override PageAccess ProjectAccess { get; set; } = new PageAccess
        {
            Name = nameof(ProjectAccess),
        };

        public override PageAccess UsersAccess { get; set; } = new PageAccess()
        {
            Name = nameof(UsersAccess),
        };

        public override PageAccess AgentsAccess { get; set; } = new PageAccess()
        {
            Name = nameof(AgentsAccess),
        };
    }

    public class AgentGroupModel : UserGroupMetadata
    {
        public AgentGroupModel(UserGroupModel userGroup)
        {
            Id = userGroup.Id;
            Name = userGroup.Name;
            StaticCode = userGroup.StaticCode;
        }

        public override PageAccess DashboardAccess { get; set; } = new PageAccess
        {
            Name = nameof(DashboardAccess)
        };
        public override PageAccess EstateAccess { get; set; } = new PageAccess
        {
            Name = nameof(EstateAccess)
        };

        public override PageAccess BasicInformationAccess { get; set; } = new PageAccess
        {
            Name = nameof(BasicInformationAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess ProjectAccess { get; set; } = new PageAccess
        {
            Name = nameof(ProjectAccess),
        };

        public override PageAccess UsersAccess { get; set; } = new PageAccess()
        {
            Name = nameof(UsersAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };

        public override PageAccess AgentsAccess { get; set; } = new PageAccess()
        {
            Name = nameof(AgentsAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false

        };
    }

    public class MarketAssistanceGroupModel : UserGroupMetadata
    {
        public MarketAssistanceGroupModel(UserGroupModel userGroup)
        {
            Id = userGroup.Id;
            Name = userGroup.Name;
            StaticCode = userGroup.StaticCode;
        }

        public override PageAccess DashboardAccess { get; set; } = new PageAccess
        {
            Name = nameof(DashboardAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess EstateAccess { get; set; } = new PageAccess
        {
            Name = nameof(EstateAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess BasicInformationAccess { get; set; } = new PageAccess
        {
            Name = nameof(BasicInformationAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess ProjectAccess { get; set; } = new PageAccess
        {
            Name = nameof(ProjectAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };

        public override PageAccess UsersAccess { get; set; } = new PageAccess()
        {
            Name = nameof(UsersAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };

        public override PageAccess AgentsAccess { get; set; } = new PageAccess()
        {
            Name = nameof(AgentsAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
    }

    public class MarketAssistancePlusGroupModel : UserGroupMetadata
    {
        public MarketAssistancePlusGroupModel(UserGroupModel userGroup)
        {
            Id = userGroup.Id;
            Name = userGroup.Name;
            StaticCode = userGroup.StaticCode;
        }

        public override PageAccess DashboardAccess { get; set; } = new PageAccess
        {
            Name = nameof(DashboardAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess EstateAccess { get; set; } = new PageAccess
        {
            Name = nameof(EstateAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess BasicInformationAccess { get; set; } = new PageAccess
        {
            Name = nameof(BasicInformationAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess ProjectAccess { get; set; } = new PageAccess
        {
            Name = nameof(ProjectAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };

        public override PageAccess UsersAccess { get; set; } = new PageAccess()
        {
            Name = nameof(UsersAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };

        public override PageAccess AgentsAccess { get; set; } = new PageAccess()
        {
            Name = nameof(AgentsAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
    }

    public class PropertyOwnerGroupModel : UserGroupMetadata
    {
        public PropertyOwnerGroupModel(UserGroupModel userGroup)
        {
            Id = userGroup.Id;
            Name = userGroup.Name;
            StaticCode = userGroup.StaticCode;
        }

        public override PageAccess DashboardAccess { get; set; } = new PageAccess
        {
            Name = nameof(DashboardAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess EstateAccess { get; set; } = new PageAccess
        {
            Name = nameof(EstateAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess BasicInformationAccess { get; set; } = new PageAccess
        {
            Name = nameof(BasicInformationAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess ProjectAccess { get; set; } = new PageAccess
        {
            Name = nameof(ProjectAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };

        public override PageAccess UsersAccess { get; set; } = new PageAccess()
        {
            Name = nameof(UsersAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };

        public override PageAccess AgentsAccess { get; set; } = new PageAccess()
        {
            Name = nameof(AgentsAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
    }

    public class AppClientGroupModel : UserGroupMetadata
    {
        public AppClientGroupModel(UserGroupModel userGroup)
        {
            Id = userGroup.Id;
            Name = userGroup.Name;
            StaticCode = userGroup.StaticCode;
        }

        public override PageAccess DashboardAccess { get; set; } = new PageAccess
        {
            Name = nameof(DashboardAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess EstateAccess { get; set; } = new PageAccess
        {
            Name = nameof(EstateAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess BasicInformationAccess { get; set; } = new PageAccess
        {
            Name = nameof(BasicInformationAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
        public override PageAccess ProjectAccess { get; set; } = new PageAccess
        {
            Name = nameof(ProjectAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };

        public override PageAccess UsersAccess { get; set; } = new PageAccess()
        {
            Name = nameof(UsersAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };

        public override PageAccess AgentsAccess { get; set; } = new PageAccess()
        {
            Name = nameof(AgentsAccess),
            AccessUpdate = false,
            AccessToPage = false,
            AccessDelete = false,
            AccessCreate = false,
            AccessGet = false
        };
    }
}