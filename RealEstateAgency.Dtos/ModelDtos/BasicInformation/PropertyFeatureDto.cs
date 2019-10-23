using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class PropertyFeatureDto : ModelDtoBase<PropertyFeature>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<PropertyFeature> From(PropertyFeature entity)
        {
            Name = entity.Name;
            Id = entity.Id;
            return this;
        }

        public override PropertyFeature Create() =>
            new PropertyFeature
            {
                Name = Name,
            };

        public override PropertyFeature Update() =>
            new PropertyFeature
            {
                Id = Id,
                Name = Name,
            };
    }
}
