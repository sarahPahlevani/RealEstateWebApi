using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories
{
    internal interface IEntityUpdateFactory<TEntity>
        where TEntity : class, IEntity
    {
        IEntityUpdateService<TEntity> Create(RealEstateDbContext context);
    }
}
