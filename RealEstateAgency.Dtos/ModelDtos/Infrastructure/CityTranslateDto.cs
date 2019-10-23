using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class CityTranslateDto : ModelDtoBase<CityTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<CityTranslate> From(CityTranslate entity)
        {
            Id = entity.Id;
            CityId = entity.CityId;
            LanguageId = entity.LanguageId;
            Name = entity.Name;
            return this;
        }

        public override CityTranslate Create() =>
            new CityTranslate
            {
                LanguageId = LanguageId,
                Name = Name,
                CityId = CityId,
            };

        public override CityTranslate Update() =>
            new CityTranslate
            {
                Id = Id,
                LanguageId = LanguageId,
                Name = Name,
                CityId = CityId,
            };
    }
}
