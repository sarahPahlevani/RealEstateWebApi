using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.DAL.DtoContracts
{
    public abstract class ModelDtoBase<TEntity> : IModelDto<TEntity>
        where TEntity : class, IEntity
    {
        public abstract int Id { get; set; }

        public abstract IModelDto<TEntity> From(TEntity entity);

        public abstract TEntity Create();

        public abstract TEntity Update();
    }
}
