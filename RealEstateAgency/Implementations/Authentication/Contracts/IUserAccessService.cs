using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.Authentication.Contracts
{
    public interface IUserAccessService<TEntity> where TEntity : class, IEntity
    {
        bool DeleteAccess { get; }
        bool CreateAccess { get; }
        bool UpdateAccess { get; }
        bool GetAccess { get; }
        bool ModifyAccess { get; }

    }
}