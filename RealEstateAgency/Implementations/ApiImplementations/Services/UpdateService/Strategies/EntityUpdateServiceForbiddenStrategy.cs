using System;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.UpdateService.Strategies
{
    public class EntityUpdateServiceForbiddenStrategy<TEntity> : EntityUpdateService<TEntity> where TEntity : class, IEntity
    {
        public override Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
            => throw _defaultError;

        public override TEntity Update(TEntity entity)
            => throw _defaultError;

        private Exception _defaultError =>
            throw new UpdateForbiddenException();
    }
}
