using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services
{
    internal class EntityService<TEntity> : IEntityService<TEntity>
    where TEntity : class, IEntity
    {
        private readonly IEntityGetFactory<TEntity> _entityGetFactory;
        private readonly IEntityGetAllFactory<TEntity> _entityGetAllFactory;
        private IEntityGetAllService<TEntity> _entityGetAllService;
        private IEntityGetService<TEntity> _getService;
        private readonly IEntityDeleteService<TEntity> _deleteService;
        private readonly IEntityUpdateService<TEntity> _updateService;
        private readonly IEntityCreateService<TEntity> _createService;

        public EntityService(RealEstateDbContext context,
            IEntityGetFactory<TEntity> entityGetFactory,
            IEntityGetAllFactory<TEntity> entityGetAllFactory,
            IEntityDeleteFactory<TEntity> entityDeleteFactory,
            IEntityUpdateFactory<TEntity> entityUpdateFactory,
            IEntityCreateFactory<TEntity> entityCreateFactory)
        {
            DbContext = context;
            _entityGetFactory = entityGetFactory;
            _entityGetAllFactory = entityGetAllFactory;
            _entityGetAllService = entityGetAllFactory.Create(context);
            _getService = entityGetFactory.Create(context);
            _deleteService = entityDeleteFactory.Create(context);
            _updateService = entityUpdateFactory.Create(context);
            _createService = entityCreateFactory.Create(context);
        }

        public RealEstateDbContext DbContext { get; }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await _updateService.UpdateAsync(entity, cancellationToken);

        public TEntity Update(TEntity entity)
            => _updateService.Update(entity);

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await _createService.CreateAsync(entity, cancellationToken);

        public TEntity Create(TEntity entity)
            => _createService.Create(entity);

        public Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => _createService.CreateRangeAsync(entities, cancellationToken);

        public void CreateRange(IEnumerable<TEntity> entities)
            => _createService.CreateRange(entities);

        public async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await _deleteService.DeleteAsync(entity, cancellationToken);

        public int Delete(TEntity entity) => _deleteService.Delete(entity);

        public Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => _deleteService.DeleteRangeAsync(entities, cancellationToken);

        public int DeleteRange(IEnumerable<TEntity> entities)
            => _deleteService.DeleteRange(entities);

        public async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default)
            => await _getService.GetAsync(id, cancellationToken);

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default)
            => _getService.GetAsync(filter, cancellationToken);

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
            => _getService.Get(filter);

        public TEntity Get(int id) => _getService.Get(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _entityGetAllService.GetAllAsync(cancellationToken);

        public IEnumerable<TEntity> GetAll() => _entityGetAllService.GetAll();

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default)
            => _entityGetAllService.GetAllAsync(filter, cancellationToken);

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
            => _entityGetAllService.GetAll(filter);

        public IQueryable<TEntity> Queryable => _entityGetAllService.Queryable;

        public IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter)
            => _entityGetAllService.AsQueryable(filter);

        public IQueryable<TEntity> AsQueryable() => _entityGetAllService.AsQueryable();

        public void SetBaseFilter(Func<IQueryable<TEntity>, IQueryable<TEntity>> baseFilter)
        {
            _getService = _entityGetFactory.Create(DbContext, baseFilter(DbContext.Set<TEntity>()));
            _entityGetAllService = _entityGetAllFactory.Create(DbContext, baseFilter(DbContext.Set<TEntity>()));
        }
    }
}
