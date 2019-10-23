using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.GetService.Strategies
{
    public class EntityGetServiceAccessDeniedStrategy<TEntity> : EntityGetService<TEntity> where TEntity : class, IEntity
    {
        public override Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default)
            => throw new AccessDeniedException($"{nameof(GetAsync)} => {nameof(TEntity)}");

        public override Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        => throw new AccessDeniedException($"{nameof(GetAsync)} => {nameof(TEntity)}");

        public override TEntity Get(Expression<Func<TEntity, bool>> filter)
            => throw new AccessDeniedException($"{nameof(Get)} => {nameof(TEntity)}");

        public override TEntity Get(int id)
            => throw new AccessDeniedException($"{nameof(Get)} => {nameof(TEntity)}");
    }
}
