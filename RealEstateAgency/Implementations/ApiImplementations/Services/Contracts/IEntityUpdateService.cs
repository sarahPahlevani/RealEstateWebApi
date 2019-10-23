using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Contracts
{
    public interface IEntityUpdateService<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        TEntity Update(TEntity entity);
    }
}
