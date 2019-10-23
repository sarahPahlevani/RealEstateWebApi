using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestTypeDto : ModelDtoBase<RequestType>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool CanAddProperty { get; set; }



        public override IModelDto<RequestType> From(RequestType entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            CanAddProperty = entity.CanAddProperty;
            return this;
        }

        public override RequestType Create() =>
            new RequestType
            {
                Name = Name,
                CanAddProperty = CanAddProperty
            };

        public override RequestType Update() =>
            new RequestType
            {
                Name = Name,
                Id = Id,
                CanAddProperty = CanAddProperty
            };
    }
}
