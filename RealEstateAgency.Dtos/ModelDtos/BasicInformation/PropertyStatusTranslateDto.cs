using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class PropertyStatusTranslateDto : ModelDtoBase<PropertyStatusTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int PropertyStatusId { get; set; }

        public PropertyStatus PropertyStatus { get; set; }

        [Required]
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<PropertyStatusTranslate> From(PropertyStatusTranslate entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            PropertyStatusId = entity.PropertyStatusId;
            PropertyStatus = entity.PropertyStatus;
            Language = entity.Language;
            LanguageId = entity.LanguageId;
            return this;
        }

        public override PropertyStatusTranslate Create() =>
            new PropertyStatusTranslate
            {
                Name = Name,
                PropertyStatusId = PropertyStatusId,
                LanguageId = LanguageId,
            };

        public override PropertyStatusTranslate Update() =>
            new PropertyStatusTranslate
            {
                Name = Name,
                PropertyStatusId = PropertyStatusId,
                LanguageId = LanguageId,
                Id = Id
            };
    }
}
