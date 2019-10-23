using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class UserAccountWishListDto : ModelDtoBase<UserAccountWishList>
    {
        public override int Id { get; set; }
        public int UserAccountId { get; set; }
        public int PropertyId { get; set; }

        public override IModelDto<UserAccountWishList> From(UserAccountWishList entity)
        {
            Id = entity.Id;
            PropertyId = entity.PropertyId;
            UserAccountId = entity.UserAccountId;
            return this;
        }

        public override UserAccountWishList Create() =>
            new UserAccountWishList
            {
                PropertyId = PropertyId,
                UserAccountId = UserAccountId
            };

        public override UserAccountWishList Update() =>
            new UserAccountWishList
            {
                Id = Id,
                PropertyId = PropertyId,
                UserAccountId = UserAccountId
            };
    }
}
