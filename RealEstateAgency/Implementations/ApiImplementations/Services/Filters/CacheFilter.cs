using System.Linq;
using EFSecondLevelCache.Core;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Filters
{
    public class CacheFilter<TEntity> : GetFilter<IQueryable<TEntity>>
    {
        public override IQueryable<TEntity> Filter(IQueryable<TEntity> filter)
            => filter.Cacheable();
    }
}
