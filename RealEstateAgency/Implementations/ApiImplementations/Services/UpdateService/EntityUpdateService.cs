using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.UpdateService
{
    public abstract class EntityUpdateService<TEntity> : IEntityUpdateService<TEntity> where TEntity : class, IEntity
    {
        public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        public abstract TEntity Update(TEntity entity);
    }
}
