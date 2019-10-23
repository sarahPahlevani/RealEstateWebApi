using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.UpdateService.Strategies
{
    public class EntityUpdateServiceNormalStrategy<TEntity> : EntityUpdateService<TEntity> where TEntity : class, IEntity
    {
        private readonly RealEstateDbContext _context;

        public EntityUpdateServiceNormalStrategy(RealEstateDbContext context) => _context = context;

        public override async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
