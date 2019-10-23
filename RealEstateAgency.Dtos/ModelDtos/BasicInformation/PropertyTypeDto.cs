using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class PropertyTypeDto : ModelDtoBase<PropertyType>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public bool CanAddProperty { get; set; }
        

        public override IModelDto<PropertyType> From(PropertyType entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            return this;
        }

        public override PropertyType Create() =>
            new PropertyType
            {
                Name = Name,
            };

        public override PropertyType Update() =>
            new PropertyType
            {
                Id = Id,
                Name = Name,
            };
    }
}
