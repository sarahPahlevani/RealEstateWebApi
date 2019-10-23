using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.GetService.Strategies
{
    public class EntityGetServiceNormalStrategy<TEntity> : EntityGetService<TEntity> where TEntity : class, IEntity
    {
        private readonly IEntityGetAllService<TEntity> _getAllService;

        public EntityGetServiceNormalStrategy(IEntityGetAllService<TEntity> getAllService) 
            => _getAllService = getAllService;

        public override async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default)
            => await AsQueryable().FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

        public override async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            => await AsQueryable().FirstOrDefaultAsync(filter, cancellationToken);

        public override TEntity Get(Expression<Func<TEntity, bool>> filter)
            => AsQueryable().FirstOrDefault(filter);

        public override TEntity Get(int id) => AsQueryable().FirstOrDefault(i => i.Id == id);

        private IQueryable<TEntity> AsQueryable() => _getAllService.Queryable;
    }
}
