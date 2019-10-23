using System.Linq;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.GetService.Strategies;
using RealEstateAgency.Implementations.Authentication.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories
{
    public class EntityGetFactory<TEntity> : IEntityGetFactory<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IUserAccessService<TEntity> _accessService;
        private readonly IEntityGetAllFactory<TEntity> _entityGetAllFactory;
        public EntityGetFactory(IUserAccessService<TEntity> accessService, IEntityGetAllFactory<TEntity> entityGetAllFactory)
        {
            _accessService = accessService;
            _entityGetAllFactory = entityGetAllFactory;
        }

        public IEntityGetService<TEntity> Create(RealEstateDbContext context, IQueryable<TEntity> baseFilter = null)
        {
            if (!_accessService.GetAccess)
                return new EntityGetServiceAccessDeniedStrategy<TEntity>();
            return new EntityGetServiceNormalStrategy<TEntity>(_entityGetAllFactory.Create(context,baseFilter));
        }
    }
}
