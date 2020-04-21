using System.Linq;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts
{
    public abstract class BasePageFilter<TEntity> : IPageFilter<TEntity>
        where TEntity : class, IEntity
    {
        public abstract IQueryable<TEntity> Filter(IQueryable<TEntity> entities);
    }

    public interface IPageFilter<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> Filter(IQueryable<TEntity> entities);
    }
}
