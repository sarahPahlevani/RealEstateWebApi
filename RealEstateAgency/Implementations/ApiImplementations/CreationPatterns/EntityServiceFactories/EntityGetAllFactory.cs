using System.Linq;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.GetAllService.Strategies;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.Authentication.Contracts;

namespace RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories
{
    public class EntityGetAllFactory<TEntity> : FactoryBase, IEntityGetAllFactory<TEntity> where TEntity : class, IEntity
    {
        private readonly IUserAccessService<TEntity> _accessService;
        private readonly IUserProvider _userProvider;
        public EntityGetAllFactory(IUserAccessService<TEntity> accessService, IUserProvider userProvider)
        {
            _accessService = accessService;
            _userProvider = userProvider;
        }

        public IEntityGetAllService<TEntity> Create(RealEstateDbContext context, IQueryable<TEntity> baseFilter = null)
        {
            if (!_accessService.GetAccess)
                return new EntityGetAllServiceAccessDeniedStrategy<TEntity>();
            return new EntityGetAllServiceNormalStrategy<TEntity>(context, _userProvider.LanguageId, baseFilter);
        }
    }
}
