using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Contracts
{
    public interface IEntityGetAllService<TEntity>
        where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> Queryable { get; }
        IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> AsQueryable();
    }
}
