using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Shared.AccessAttributes;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.DAL.Models
{
    [CreateAccess(UserGroups.Administrator), UpdateAccess(UserGroups.Administrator, UserGroups.RealEstateAdministrator),
     DeleteAccess(UserGroups.Administrator)]
    public partial class RealEstate : IEntity
    {
    }
}
