using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{

    [GetAccess(UserGroups.Administrator, UserGroups.Agent, UserGroups.RealEstateAdministrator)]
    [ModifyAccess(UserGroups.Administrator, UserGroups.Agent, UserGroups.RealEstateAdministrator)]
    public partial class PropertyAroundImportantLandmark : IVirtualDelete
    {
    }
}
