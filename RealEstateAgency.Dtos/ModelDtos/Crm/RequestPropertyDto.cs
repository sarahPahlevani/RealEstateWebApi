using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Crm
{
    public class RequestPropertyDto : ModelDtoBase<RequestProperty>
    {
        public override int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public string Description { get; set; }

        public override IModelDto<RequestProperty> From(RequestProperty entity)
        {
            Id = entity.Id;
            RequestId = entity.RequestId;
            Description = entity.Description;
            PropertyId = entity.RequestId;
            return this;
        }

        public override RequestProperty Create() =>
            new RequestProperty
            {
                Description = Description,
                PropertyId = PropertyId,
                RequestId = RequestId,
            };

        public override RequestProperty Update() =>
            new RequestProperty
            {
                Id = Id,
                Description = Description,
                PropertyId = PropertyId,
                RequestId = RequestId,
            };
    }
}
