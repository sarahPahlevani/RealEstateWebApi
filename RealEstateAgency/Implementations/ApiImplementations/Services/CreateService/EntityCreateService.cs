using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.CreateService
{
    public abstract class EntityCreateService<TEntity> : IEntityCreateService<TEntity> where TEntity : class, IEntity
    {
        public abstract Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        public abstract TEntity Create(TEntity entity);

        public abstract Task CreateRangeAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default);

        public abstract void CreateRange(IEnumerable<TEntity> entities);
    }
}
