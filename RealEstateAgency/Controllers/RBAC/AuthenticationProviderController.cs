using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.RBAC
{
    public class AuthenticationProviderController : ModelController<AuthenticationProvider, AuthenticationProviderDto>
    {
        public AuthenticationProviderController(IModelService<AuthenticationProvider, AuthenticationProviderDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<AuthenticationProvider>, IQueryable<AuthenticationProviderDto>> DtoConverter
        => items => items.Select(i => new AuthenticationProviderDto
        {
            Id = i.Id,
            Name = i.Name
        });
    }
}
