using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.CreateService.Strategies
{
    public class EntityCreateServiceNormalStrategy<TEntity> : EntityCreateService<TEntity> where TEntity : class, IEntity
    {
        private readonly RealEstateDbContext _context;

        public EntityCreateServiceNormalStrategy(RealEstateDbContext context) => _context = context;

        public override async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var item = await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            entity.Id = item.Entity.Id;
            return entity;
        }

        public override TEntity Create(TEntity entity)
        {
            var item = _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            entity.Id = item.Entity.Id;
            return entity;
        }

        public override async Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().AddRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public override void CreateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }
    }
}
