using System;
using System.Linq;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.DtoContracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Contracts
{
    public interface IModelService<TEntity, TDto> : IEntityService<TEntity>, IDtoService<TDto>
        where TEntity : class, IEntity
        where TDto : class, IModelDto<TEntity>, new()
    {
        void SetQuery(Func<IQueryable<TEntity>, IQueryable<TDto>> func);

        Func<IQueryable<TEntity>, IQueryable<TDto>> DataConvertQuery { set; get; }
        Func<TEntity, TDto> CreateDtoFlow { get; set; }
        Func<TDto, TEntity> CreateModelFlow { get; set; }
        Func<TDto, TEntity> UpdateModelFlow { get; set; }

        void Configure(Action<IModelService<TEntity, TDto>> configuration);
    }
}
