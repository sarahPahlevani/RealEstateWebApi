using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.CreateService.Strategies;
using RealEstateAgency.Implementations.Authentication.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories
{
    internal class EntityCreateFactory<TEntity> : FactoryBase, IEntityCreateFactory<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IUserAccessService<TEntity> _accessService;

        public EntityCreateFactory(IUserAccessService<TEntity> accessService)
        {
            _accessService = accessService;
        }

        public IEntityCreateService<TEntity> Create(RealEstateDbContext context)
        {
            //if (!_accessService.CreateAccess)
            //    return new EntityCreateServiceAccessDenied<TEntity>();
            //if (IsForbiddenCreate<TEntity>())
            //    return new EntityCreateServiceForbidden<TEntity>();
            return new EntityCreateServiceNormalStrategy<TEntity>(context);
        }
    }
}
