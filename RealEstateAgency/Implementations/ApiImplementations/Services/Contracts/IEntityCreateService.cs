using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Contracts
{
    public interface IEntityCreateService<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        TEntity Create(TEntity entity);

        Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        void CreateRange(IEnumerable<TEntity> entities);
    }
}
