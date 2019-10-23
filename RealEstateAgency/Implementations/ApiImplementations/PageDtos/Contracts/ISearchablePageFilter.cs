using System.Linq;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts
{
    public interface ISearchablePageFilter<TEntity>
        where TEntity : class, IEntity
    {
        string SearchText { get; set; }

        IQueryable<TEntity> SearchFilter(IQueryable<TEntity> properties);
    }
    public interface ISpecialSearchablePageFilter<TEntity> : ISearchablePageFilter<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> SpecialSearchByFlags(IQueryable<TEntity> properties);
        bool IsContainSpecialFlags();
    }
}
