using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Contracts
{
    public interface IEntityDeleteService<in TEntity>
        where TEntity : class, IEntity
    {
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        int Delete(TEntity entity);

        Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        int DeleteRange(IEnumerable<TEntity> entities);
    }
}
