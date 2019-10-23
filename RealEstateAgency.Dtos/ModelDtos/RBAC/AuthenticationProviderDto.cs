using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class AuthenticationProviderDto : ModelDtoBase<AuthenticationProvider>
    {
        public override int Id { get; set; }
        public string Name { get; set; }

        public override IModelDto<AuthenticationProvider> From(AuthenticationProvider entity)
        {
            Id = entity.Id;
            Name = Name;
            return this;
        }

        public override AuthenticationProvider Create() =>
            new AuthenticationProvider
            {
                Name = Name
            };

        public override AuthenticationProvider Update() =>
            new AuthenticationProvider
            {
                Id = Id,
                Name = Name
            };
    }
}
