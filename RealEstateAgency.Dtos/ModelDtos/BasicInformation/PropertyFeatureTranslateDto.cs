using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class PropertyFeatureTranslateDto : ModelDtoBase<PropertyFeatureTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int PropertyFeatureId { get; set; }

        public PropertyFeature PropertyFeature { get; set; }

        [Required]
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<PropertyFeatureTranslate> From(PropertyFeatureTranslate entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            LanguageId = entity.LanguageId;
            Language = entity.Language;
            PropertyFeatureId = entity.PropertyFeatureId;
            PropertyFeature = entity.PropertyFeature;
            return this;
        }

        public override PropertyFeatureTranslate Create() =>
            new PropertyFeatureTranslate
            {
                Name = Name,
                LanguageId = LanguageId,
                PropertyFeatureId = PropertyFeatureId
            };

        public override PropertyFeatureTranslate Update() =>
            new PropertyFeatureTranslate
            {
                Id = Id,
                Name = Name,
                LanguageId = LanguageId,
                PropertyFeatureId = PropertyFeatureId
            };
    }
}
