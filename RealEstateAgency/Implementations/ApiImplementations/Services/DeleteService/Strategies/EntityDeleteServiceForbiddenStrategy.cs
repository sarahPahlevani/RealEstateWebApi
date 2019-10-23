using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.DeleteService.Strategies
{
    public class EntityDeleteServiceForbiddenStrategy<TEntity> : EntityDeleteService<TEntity> where TEntity : class, IEntity
    {
        public override Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
            => throw _defaultError;

        public override int Delete(TEntity entity)
            => throw _defaultError;

        public override Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => throw _defaultError;

        public override int DeleteRange(IEnumerable<TEntity> entities)
            => throw _defaultError;

        private Exception _defaultError =>
            throw new DeleteForbiddenException();
    }
}
