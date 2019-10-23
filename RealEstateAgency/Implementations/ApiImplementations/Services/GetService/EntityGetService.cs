using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.GetService
{
    public abstract class EntityGetService<TEntity> : BaseGetService<TEntity>, IEntityGetService<TEntity> where TEntity : class, IEntity
    {
        public abstract Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default);

        public abstract Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default);

        public abstract TEntity Get(Expression<Func<TEntity, bool>> filter);
        public abstract TEntity Get(int id);
    }
}
