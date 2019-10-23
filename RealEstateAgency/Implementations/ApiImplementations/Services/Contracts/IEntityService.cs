using System;
using System.Linq;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Contracts
{
    public interface IEntityService<TEntity> : IEntityUpdateService<TEntity>, IEntityCreateService<TEntity>,
        IEntityDeleteService<TEntity>, IEntityGetService<TEntity>, IEntityGetAllService<TEntity>
        where TEntity : class, IEntity
    {
        void SetBaseFilter(Func<IQueryable<TEntity>, IQueryable<TEntity>> baseFilter);

        RealEstateDbContext DbContext { get; }
    }
}
