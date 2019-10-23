using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.DeleteService.Strategies
{
    public class EntityDeleteServiceAccessDeniedStrategy<TEntity> : EntityDeleteService<TEntity> where TEntity : class, IEntity
    {
        public override Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
            => throw new AccessDeniedException($"{nameof(DeleteAsync)} => {nameof(TEntity)}");

        public override int Delete(TEntity entity)
            => throw new AccessDeniedException($"{nameof(Delete)} => {nameof(TEntity)}");

        public override Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => throw new AccessDeniedException($"{nameof(DeleteRangeAsync)} => {nameof(TEntity)}");

        public override int DeleteRange(IEnumerable<TEntity> entities)
            => throw new AccessDeniedException($"{nameof(DeleteRange)} => {nameof(TEntity)}");

    }
}
