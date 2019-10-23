using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.DAL.DtoContracts
{
    public interface ICreatable<out TEntity>
        where TEntity : class, IEntity
    {
        TEntity Create();
    }
}
