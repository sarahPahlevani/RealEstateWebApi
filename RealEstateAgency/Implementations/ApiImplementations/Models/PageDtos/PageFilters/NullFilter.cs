using System.Linq;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters
{
    public class NullFilter<TEntity> : BasePageFilter<TEntity> where TEntity : class, IEntity
    {
        public override IQueryable<TEntity> Filter(IQueryable<TEntity> entities) => entities;
    }
}
