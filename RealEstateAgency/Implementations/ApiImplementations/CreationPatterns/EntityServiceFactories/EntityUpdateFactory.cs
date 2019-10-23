using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.UpdateService.Strategies;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.Authentication.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories
{
    internal class EntityUpdateFactory<TEntity> : FactoryBase, IEntityUpdateFactory<TEntity> where TEntity : class, IEntity
    {
        private readonly IUserAccessService<TEntity> _accessService;
        private readonly IUserProvider _userProvider;

        public EntityUpdateFactory(IUserAccessService<TEntity> accessService, IUserProvider userProvider)
        {
            _accessService = accessService;
            _userProvider = userProvider;
        }

        public IEntityUpdateService<TEntity> Create(RealEstateDbContext context)
        {

            if (!_accessService.UpdateAccess)
                return new EntityUpdateServiceAccessDeniedStrategy<TEntity>();
            if (IsForbiddenUpdate<TEntity>())
                return new EntityUpdateServiceForbiddenStrategy<TEntity>();
            if (IsVirtualDelete<TEntity>()) //TODO: user id should add here
                return new EntityUpdateServiceVirtualStrategy<TEntity>(context, null);
            return new EntityUpdateServiceNormalStrategy<TEntity>(context);
        }
    }
}
