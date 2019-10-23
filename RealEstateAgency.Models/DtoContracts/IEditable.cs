using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.DAL.DtoContracts
{
    public interface IEditable<out TEntity>
        where TEntity : class, IEntity
    {
        TEntity Update();
    }
}
