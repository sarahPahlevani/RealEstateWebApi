using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.DeleteService
{
    public abstract class EntityDeleteService<TEntity> : IEntityDeleteService<TEntity> where TEntity : class, IEntity
    {
        public abstract Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        public abstract int Delete(TEntity entity);
        public abstract Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        public abstract int DeleteRange(IEnumerable<TEntity> entities);
    }
}
