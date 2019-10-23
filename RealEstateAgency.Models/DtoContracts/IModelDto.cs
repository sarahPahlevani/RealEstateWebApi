using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.DAL.DtoContracts
{
    public interface IModelDto<TEntity>
        : IDto, ICreatable<TEntity>, IEditable<TEntity>
    where TEntity : class, IEntity
    {
        int Id { get; set; }

        IModelDto<TEntity> From(TEntity entity);
    }
}
