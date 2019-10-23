using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.CreateService.Strategies
{
    public class EntityCreateServiceAccessDenied<TEntity> : EntityCreateService<TEntity> where TEntity : class, IEntity
    {
        public override Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
            => throw new AccessDeniedException($"{nameof(CreateAsync)} => {nameof(TEntity)}");

        public override TEntity Create(TEntity entity)
            => throw new AccessDeniedException($"{nameof(Create)} => {nameof(TEntity)}");

        public override Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => throw new AccessDeniedException($"{nameof(CreateRangeAsync)} => {nameof(TEntity)}");

        public override void CreateRange(IEnumerable<TEntity> entities)
            => throw new AccessDeniedException($"{nameof(CreateRange)} => {nameof(TEntity)}");
    }
}
