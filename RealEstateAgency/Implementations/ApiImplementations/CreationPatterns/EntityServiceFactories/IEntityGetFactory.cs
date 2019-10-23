using System.Linq;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories
{
    internal interface IEntityGetFactory<TEntity>
        where TEntity : class, IEntity
    {
        IEntityGetService<TEntity> Create(RealEstateDbContext context, IQueryable<TEntity> baseFilter = null);
    }
}
