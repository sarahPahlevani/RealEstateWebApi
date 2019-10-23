using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Shared.Exceptions;

namespace RealEstateAgency.Implementations.ApiImplementations.Services
{
    public class ModelService<TEntity, TDto> : IModelService<TEntity, TDto>
        where TEntity : class, IEntity
        where TDto : class, IModelDto<TEntity>, new()
    {
        private readonly IEntityService<TEntity> _entityService;

        public Func<IQueryable<TEntity>, IQueryable<TDto>> DataConvertQuery { set; get; }
        public Func<TDto, TEntity> CreateModelFlow { get; set; } = dto => dto.Create();
        public Func<TEntity, TDto> CreateDtoFlow { get; set; } = entity => (TDto)new TDto().From(entity);
        public Func<TDto, TEntity> UpdateModelFlow { get; set; } = dto => dto.Update();

        public ModelService(IEntityService<TEntity> entityService)
            => _entityService = entityService;

        public async Task<IEnumerable<TDto>> GetAllDtosAsync(CancellationToken cancellationToken = default)
        {
            var res = _entityService.Queryable;
            return await DataConvertQuery(res).ToListAsync(cancellationToken);
        }

        public IEnumerable<TDto> GetAllDtos()
        {
            var res = _entityService.Queryable;
            return DataConvertQuery(res).ToList();
        }

        public TDto GetDto(int id)
        {
            var res = _entityService.Get(id);
            if (res is null) return null;
            return CreateDtoFlow(res);
        }

        public async Task<TDto> GetDtoAsync(int id, CancellationToken cancellationToken = default)
        {
            var res = await _entityService.GetAsync(id, cancellationToken);
            if (res is null) return null;
            return CreateDtoFlow(res);
        }

        public TDto CreateByDto(TDto value)
        {
            var item = _entityService.Create(UpdateModelFlow(value));
            value.Id = item.Id;
            return value;
        }

        public async Task<TDto> CreateByDtoAsync(TDto value, CancellationToken cancellationToken = default)
        {
            var item = await _entityService.CreateAsync(value.Create(), cancellationToken);
            value.Id = item.Id;
            return value;
        }

        public void UpdateByDto(TDto value) => _entityService.Update(value.Update());

        public async Task UpdateByDtoAsync(TDto value, CancellationToken cancellationToken = default)
            => await _entityService.UpdateAsync(value.Update(), cancellationToken);

        public int DeleteById(int id)
        {
            var item = _entityService.Get(id);
            return _entityService.Delete(item);
        }

        public async Task<int> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await _entityService.GetAsync(id, cancellationToken);
            if (item is null) throw new AppNotFoundException("Cannot find the entity for delete");
            return await _entityService.DeleteAsync(item, cancellationToken);
        }

        public void SetQuery(Func<IQueryable<TEntity>, IQueryable<TDto>> func) => DataConvertQuery = func;

        public void Configure(Action<IModelService<TEntity, TDto>> configuration)
            => configuration(this);

        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
            => _entityService.UpdateAsync(entity);

        public TEntity Update(TEntity entity) => _entityService.Update(entity);

        public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
            => _entityService.CreateAsync(entity, cancellationToken);

        public TEntity Create(TEntity entity)
            => _entityService.Create(entity);

        public Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => _entityService.CreateRangeAsync(entities, cancellationToken);

        public void CreateRange(IEnumerable<TEntity> entities)
            => _entityService.CreateRange(entities);

        public Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
            => _entityService.DeleteAsync(entity, cancellationToken);

        public int Delete(TEntity entity)
            => _entityService.Delete(entity);

        public Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => _entityService.DeleteRangeAsync(entities, cancellationToken);

        public int DeleteRange(IEnumerable<TEntity> entities)
            => _entityService.DeleteRange(entities);

        public void SetBaseFilter(Func<IQueryable<TEntity>, IQueryable<TEntity>> baseFilter)
            => _entityService.SetBaseFilter(baseFilter);

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
            => _entityService.Get(filter);

        TEntity IEntityGetService<TEntity>.Get(int id)
            => _entityService.Get(id);

        public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => _entityService.GetAllAsync(cancellationToken);

        public IEnumerable<TEntity> GetAll() => _entityService.GetAll();

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default)
            => _entityService.GetAllAsync(filter, cancellationToken);

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
            => _entityService.GetAll(filter);

        public IQueryable<TEntity> Queryable => _entityService.Queryable;

        public IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter)
            => _entityService.AsQueryable(filter);

        public IQueryable<TEntity> AsQueryable()
            => _entityService.AsQueryable();

        Task<TEntity> IEntityGetService<TEntity>.GetAsync(int id, CancellationToken cancellationToken)
            => _entityService.GetAsync(id, cancellationToken);

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default)
            => _entityService.GetAsync(filter, cancellationToken);

        public RealEstateDbContext DbContext => _entityService.DbContext;
    }
}
