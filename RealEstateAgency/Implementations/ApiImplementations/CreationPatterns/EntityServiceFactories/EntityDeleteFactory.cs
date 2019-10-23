using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.DeleteService.Strategies;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.Authentication.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories
{
    public class EntityDeleteFactory<TEntity> : FactoryBase, IEntityDeleteFactory<TEntity> where TEntity : class, IEntity
    {
        private readonly IUserAccessService<TEntity> _accessService;
        private readonly IUserProvider _userProvider;

        public EntityDeleteFactory(IUserAccessService<TEntity> accessService,IUserProvider userProvider )
        {
            _accessService = accessService;
            _userProvider = userProvider;
        }

        public IEntityDeleteService<TEntity> Create(RealEstateDbContext context)
        {
            if (!_accessService.DeleteAccess)
                return new EntityDeleteServiceAccessDeniedStrategy<TEntity>();
            if (IsForbiddenDelete<TEntity>())
                return new EntityDeleteServiceForbiddenStrategy<TEntity>();
            if (IsVirtualDelete<TEntity>())
                return new EntityDeleteServiceVirtualStrategy<TEntity>(context, _userProvider.Id);
            return new EntityDeleteServiceNormalStrategy<TEntity>(context);
        }
    }
}
