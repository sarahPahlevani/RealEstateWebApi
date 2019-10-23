using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories
{
    public interface IEntityDeleteFactory<in TEntity>
        where TEntity : class, IEntity
    {
        IEntityDeleteService<TEntity> Create(RealEstateDbContext context);
    }
}
