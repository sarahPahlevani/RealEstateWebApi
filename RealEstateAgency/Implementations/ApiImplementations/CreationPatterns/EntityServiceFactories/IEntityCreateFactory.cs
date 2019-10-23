using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories
{
    internal interface IEntityCreateFactory<TEntity>
        where TEntity : class, IEntity
    {
        IEntityCreateService<TEntity> Create(RealEstateDbContext context);
    }
}
