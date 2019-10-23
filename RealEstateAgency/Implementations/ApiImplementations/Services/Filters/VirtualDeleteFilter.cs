using System.Linq;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Filters
{
    internal class VirtualDeleteFilter<TEntity> : GetFilter<IQueryable<TEntity>>
        where TEntity : class, IVirtualDelete
    {
        public override IQueryable<TEntity> Filter(IQueryable<TEntity> filter)
            => filter.Where(i => i.Deleted == false);
    }
}
