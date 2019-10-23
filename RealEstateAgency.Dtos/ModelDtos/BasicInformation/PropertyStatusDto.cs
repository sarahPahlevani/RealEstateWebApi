using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class PropertyStatusDto : ModelDtoBase<PropertyStatus>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<PropertyStatus> From(PropertyStatus entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            return this;
        }

        public override PropertyStatus Create() =>
            new PropertyStatus
            {
                Name = Name
            };

        public override PropertyStatus Update() =>
            new PropertyStatus
            {
                Name = Name,
                Id = Id
            };
    }
}
