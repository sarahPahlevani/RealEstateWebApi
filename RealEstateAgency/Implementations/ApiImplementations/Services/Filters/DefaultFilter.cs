using System.Linq;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Filters
{
    internal class DefaultFilter<TEntity> : GetFilter<IQueryable<TEntity>>
    {
        public override IQueryable<TEntity> Filter(IQueryable<TEntity> filter) => filter;
    }
}
