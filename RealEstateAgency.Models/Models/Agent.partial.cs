using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{
    [UpdateAccess(UserGroups.Administrator, UserGroups.Agent, UserGroups.RealEstateAdministrator),
     CreateAccess(UserGroups.Administrator, UserGroups.RealEstateAdministrator),
     DeleteAccess(UserGroups.Administrator, UserGroups.RealEstateAdministrator)]
    public partial class Agent : IVirtualDelete
    {
    }
}
