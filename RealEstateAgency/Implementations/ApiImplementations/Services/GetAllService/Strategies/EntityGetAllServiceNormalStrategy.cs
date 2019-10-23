using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Filters;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.GetAllService.Strategies
{
    public class EntityGetAllServiceNormalStrategy<TEntity> : EntityGetAllService<TEntity> where TEntity : class, IEntity
    {
        public EntityGetAllServiceNormalStrategy(RealEstateDbContext context, int? language = null
            , IQueryable<TEntity> baseFilter = null)
        {
            if (baseFilter is null)
                baseFilter = context.Set<TEntity>();
            var filterSystem = new DefaultFilter<TEntity>();
            if (IsVirtualDelete())
            {
                var d1 = typeof(VirtualDeleteFilter<>);
                Type[] typeArgs = { typeof(TEntity) };
                var o = Activator.CreateInstance(d1.MakeGenericType(typeArgs));
                filterSystem.SetNext((IGetFilter<IQueryable<TEntity>>)o);
            }
            if (IsTranslatable())
                filterSystem.SetNext(new TranslateFilter<TEntity>(language));
            if (IsResultCache())
                filterSystem.SetNext(new CacheFilter<TEntity>());
            Queryable = filterSystem.Run(baseFilter);
        }

        public override async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await AsQueryable().ToListAsync(cancellationToken);

        public override IEnumerable<TEntity> GetAll() => AsQueryable().ToList();
        public override async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            => await AsQueryable().Where(filter).ToListAsync(cancellationToken);

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
            => AsQueryable().Where(filter).ToList();

        public override IQueryable<TEntity> Queryable { get; }

        public override IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter)
            => Queryable.Where(filter);

        public override IQueryable<TEntity> AsQueryable() => Queryable;
    }
}
