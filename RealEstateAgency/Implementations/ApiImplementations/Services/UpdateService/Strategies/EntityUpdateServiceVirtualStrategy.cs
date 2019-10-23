using System;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.UpdateService.Strategies
{
    public class EntityUpdateServiceVirtualStrategy<TEntity> : EntityUpdateServiceNormalStrategy<TEntity> where TEntity : class, IEntity
    {
        private readonly int? _userId;

        public EntityUpdateServiceVirtualStrategy(RealEstateDbContext context, int? userId) : base(context)
        {
            _userId = userId;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (((IVirtualDelete)entity).Deleted)
                VirtualDelete((IVirtualDelete)entity);
            return await base.UpdateAsync(entity, cancellationToken);
        }

        public override TEntity Update(TEntity entity)
        {
            if (((IVirtualDelete)entity).Deleted)
                VirtualDelete((IVirtualDelete)entity);
            return base.Update(entity);
        }

        private void VirtualDelete<TVirtualDelete>(TVirtualDelete entity)
            where TVirtualDelete : class, IVirtualDelete
        {
            entity.DeletedDate = DateTime.Now;
            entity.Deleted = true;
            //TODO: this section should be changed when we have user claims
            entity.UserAccountIdDeleteBy = _userId;
        }
    }
}
