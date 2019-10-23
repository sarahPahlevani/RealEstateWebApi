using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{
    [ModifyAccess(UserGroups.Administrator, UserGroups.RealEstateAdministrator
        , UserGroups.Agent)]
    [GetAccess(UserGroups.Administrator, UserGroups.Agent, UserGroups.RealEstateAdministrator)]
    public partial class RequestAction : IEntity
    {
    }
}
