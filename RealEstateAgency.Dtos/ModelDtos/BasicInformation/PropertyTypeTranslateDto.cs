using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class PropertyTypeTranslateDto : ModelDtoBase<PropertyTypeTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int PropertyTypeId { get; set; }

        public PropertyType PropertyType { get; set; }

        [Required]
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<PropertyTypeTranslate> From(PropertyTypeTranslate entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            PropertyTypeId = entity.PropertyTypeId;
            PropertyType = entity.PropertyType;
            LanguageId = entity.LanguageId;
            Language = entity.Language;
            return this;
        }

        public override PropertyTypeTranslate Create() =>
            new PropertyTypeTranslate
            {
                LanguageId = LanguageId,
                Name = Name,
                PropertyTypeId = PropertyTypeId
            };

        public override PropertyTypeTranslate Update() =>
            new PropertyTypeTranslate
            {
                LanguageId = LanguageId,
                Name = Name,
                PropertyTypeId = PropertyTypeId,
                Id = Id
            };
    }
}
