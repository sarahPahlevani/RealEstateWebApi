using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.DeleteService.Strategies
{
    public class EntityDeleteServiceNormalStrategy<TEntity> : EntityDeleteService<TEntity> where TEntity : class, IEntity
    {
        private readonly RealEstateDbContext _context;

        public EntityDeleteServiceNormalStrategy(RealEstateDbContext context) => _context = context;

        public override async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public override int Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChanges();
        }

        public override async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public override int DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return _context.SaveChanges();
        }
    }
}
