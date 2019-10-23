using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.GetAllService
{
    public abstract class EntityGetAllService<TEntity> : BaseGetService<TEntity>, IEntityGetAllService<TEntity> where TEntity : class, IEntity
    {
        public abstract Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        public abstract IEnumerable<TEntity> GetAll();

        public abstract Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default);

        public abstract IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        public abstract IQueryable<TEntity> Queryable { get; }
        public abstract IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter);

        public abstract IQueryable<TEntity> AsQueryable();
    }
}
