using System.Linq;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts
{
    public interface IAscendingPageFilter<TEntity>
        where TEntity : class, IEntity
    {
        bool IsAscending { get; set; }

        IQueryable<TEntity> AscendingFilter(IQueryable<TEntity> properties);
    }
}
