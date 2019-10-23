using System.Linq;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories
{
    public interface IEntityGetAllFactory<TEntity>
        where TEntity : class, IEntity
    {
        IEntityGetAllService<TEntity> Create(RealEstateDbContext context, IQueryable<TEntity> baseFilter = null);
    }
}
