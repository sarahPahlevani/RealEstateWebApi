using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class CountryTranslateDto : ModelDtoBase<CountryTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<CountryTranslate> From(CountryTranslate entity)
        {
            Id = entity.Id;
            CountryId = entity.CountryId;
            LanguageId = entity.LanguageId;
            Name = entity.Name;
            return this;
        }

        public override CountryTranslate Create() =>
            new CountryTranslate
            {
                LanguageId = LanguageId,
                Name = Name,
                CountryId = CountryId,
            };

        public override CountryTranslate Update() =>
            new CountryTranslate
            {
                Id = Id,
                LanguageId = LanguageId,
                CountryId = CountryId,
                Name = Name
            };
    }
}
