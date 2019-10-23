using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.BasicInformation
{
    public class ImportantPlaceTypeTranslateDto : ModelDtoBase<ImportantPlaceTypeTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int ImportantPlaceTypeId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<ImportantPlaceTypeTranslate> From(ImportantPlaceTypeTranslate entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ImportantPlaceTypeId = entity.ImportantPlaceTypeId;
            LanguageId = entity.LanguageId;
            return this;
        }

        public override ImportantPlaceTypeTranslate Create() =>
            new ImportantPlaceTypeTranslate
            {
                Name = Name,
                LanguageId = LanguageId,
                ImportantPlaceTypeId = ImportantPlaceTypeId,
            };

        public override ImportantPlaceTypeTranslate Update() =>
            new ImportantPlaceTypeTranslate
            {
                Id = Id,
                Name = Name,
                LanguageId = LanguageId,
                ImportantPlaceTypeId = ImportantPlaceTypeId,
            };
    }
}
