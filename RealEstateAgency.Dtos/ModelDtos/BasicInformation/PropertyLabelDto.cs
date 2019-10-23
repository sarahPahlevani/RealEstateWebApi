using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class PropertyLabelDto : ModelDtoBase<PropertyLabel>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<PropertyLabel> From(PropertyLabel entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            return this;
        }

        public override PropertyLabel Create() =>
            new PropertyLabel
            {
                Name = Name,
            };

        public override PropertyLabel Update()
        => new PropertyLabel
        {
            Name = Name,
            Id = Id
        };
    }
}
