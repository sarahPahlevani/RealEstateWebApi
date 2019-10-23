using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.UpdateService.Strategies
{
    public class EntityUpdateServiceAccessDeniedStrategy<TEntity> : EntityUpdateService<TEntity> where TEntity : class, IEntity
    {
        public override Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
            => throw new AccessDeniedException($"{nameof(UpdateAsync)} => {nameof(TEntity)}");

        public override TEntity Update(TEntity entity)
            => throw new AccessDeniedException($"{nameof(Update)} => {nameof(TEntity)}");
    }
}
