using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{
    [ModifyAccess(UserGroups.Administrator, UserGroups.RealEstateAdministrator
        , UserGroups.Agent)]
    [CreateAccess(UserGroups.AppClient, UserGroups.Administrator, UserGroups.Agent,
        UserGroups.RealEstateAdministrator, UserGroups.MarketAssistance, UserGroups.MarketAssistancePlus)]
    public partial class Request : IVirtualDelete
    {
    }
}
