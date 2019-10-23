using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Contracts
{
    public interface IEntityGetService<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default);

        TEntity Get(Expression<Func<TEntity, bool>> filter);

        TEntity Get(int id);
    }
}
