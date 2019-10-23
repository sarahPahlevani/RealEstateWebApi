using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.GetAllService.Strategies
{
    public class EntityGetAllServiceAccessDeniedStrategy<TEntity> : EntityGetAllService<TEntity> where TEntity : class, IEntity
    {
        public override Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => throw new AccessDeniedException($"{nameof(GetAllAsync)} => {nameof(TEntity)}");

        public override IEnumerable<TEntity> GetAll()
            => throw new AccessDeniedException($"{nameof(GetAll)} => {nameof(TEntity)}");

        public override Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            => throw new AccessDeniedException($"{nameof(GetAllAsync)} => {nameof(TEntity)}");

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
            => throw new AccessDeniedException($"{nameof(GetAll)} => {nameof(TEntity)}");

        public override IQueryable<TEntity> Queryable => throw new AccessDeniedException($"{nameof(GetAll)} => {nameof(TEntity)}");

        public override IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter)
            => throw new AccessDeniedException($"{nameof(GetAll)} => {nameof(TEntity)}");

        public override IQueryable<TEntity> AsQueryable()
            => throw new AccessDeniedException($"{nameof(AsQueryable)} => {nameof(TEntity)}");
    }
}
