using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class PropertyTypeDto : ModelDtoBase<PropertyType>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool CanAddProperty { get; set; }

        public int PropertyCount { get; set; }

        public string Icon { get; set; }


        public override IModelDto<PropertyType> From(PropertyType entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Icon = entity.Icon;
            return this;
        }

        public override PropertyType Create() =>
            new PropertyType
            {
                Name = Name,
                Icon = Icon,
            };

        public override PropertyType Update() =>
            new PropertyType
            {
                Id = Id,
                Name = Name,
                Icon = Icon,
            };
    }
}
