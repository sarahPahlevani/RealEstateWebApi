using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.CreateService.Strategies
{
    public class EntityCreateServiceForbidden<TEntity> : EntityCreateService<TEntity> where TEntity : class, IEntity
    {
        public override Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
            => throw new CreateForbiddenException();

        public override TEntity Create(TEntity entity) => throw new CreateForbiddenException();

        public override Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => throw new CreateForbiddenException();

        public override void CreateRange(IEnumerable<TEntity> entities)
            => throw new CreateForbiddenException();

    }
}
