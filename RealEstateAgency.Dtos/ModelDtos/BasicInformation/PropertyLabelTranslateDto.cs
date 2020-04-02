using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class PropertyLabelTranslateDto : ModelDtoBase<PropertyLabelTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int PropertyLabelId { get; set; }

        public PropertyLabel PropertyLabel { get; set; }

        [Required]
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<PropertyLabelTranslate> From(PropertyLabelTranslate entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            LanguageId = entity.LanguageId;
            Language = entity.Language;
            PropertyLabelId = entity.PropertyLabelId;
            PropertyLabel = entity.PropertyLabel;
            return this;
        }

        public override PropertyLabelTranslate Create() =>
            new PropertyLabelTranslate
            {
                LanguageId = LanguageId,
                Name = Name,
                PropertyLabelId = PropertyLabelId,
            };

        public override PropertyLabelTranslate Update() =>
            new PropertyLabelTranslate
            {
                LanguageId = LanguageId,
                Name = Name,
                PropertyLabelId = PropertyLabelId,
                Id = Id
            };
    }
}
