using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.DeleteService.Strategies
{
    public class EntityDeleteServiceVirtualStrategy<TEntity> : EntityDeleteService<TEntity> where TEntity : class, IEntity
    {
        private readonly RealEstateDbContext _context;
        private readonly int? _userId;

        public EntityDeleteServiceVirtualStrategy(RealEstateDbContext context, int? userId = null)
        {
            _context = context;
            _userId = userId;
        }

        public override async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            VirtualDelete((IVirtualDelete)entity);
            _context.Set<TEntity>().Update(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public override int Delete(TEntity entity)
        {
            VirtualDelete((IVirtualDelete)entity);
            _context.Set<TEntity>().Update(entity);
            return _context.SaveChanges();
        }

        public override Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
                VirtualDelete((IVirtualDelete)entity);
            _context.Set<TEntity>().UpdateRange(entities);
            return _context.SaveChangesAsync(cancellationToken);
        }

        public override int DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                VirtualDelete((IVirtualDelete)entity);
            _context.Set<TEntity>().UpdateRange(entities);
            return _context.SaveChanges();

        }

        private void VirtualDelete<TVirtualDelete>(TVirtualDelete entity)
            where TVirtualDelete : class, IVirtualDelete
        {
            entity.DeletedDate = DateTime.UtcNow;
            entity.Deleted = true;
            entity.UserAccountIdDeleteBy = _userId;
        }
    }
}
