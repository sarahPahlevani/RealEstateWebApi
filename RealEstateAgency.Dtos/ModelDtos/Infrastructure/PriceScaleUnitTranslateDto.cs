using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class PriceScaleUnitTranslateDto : ModelDtoBase<PriceScaleUnitTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int PriceScaleUnitId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<PriceScaleUnitTranslate> From(PriceScaleUnitTranslate entity)
        {
            Id = entity.Id;
            PriceScaleUnitId = entity.PriceScaleUnitId;
            LanguageId = entity.LanguageId;
            Name = entity.Name;
            return this;
        }

        public override PriceScaleUnitTranslate Create() =>
        new PriceScaleUnitTranslate
        {
            Name = Name,
            LanguageId = LanguageId,
            PriceScaleUnitId = PriceScaleUnitId,
        };

        public override PriceScaleUnitTranslate Update() =>
            new PriceScaleUnitTranslate
            {
                Id = Id,
                Name = Name,
                LanguageId = LanguageId,
                PriceScaleUnitId = PriceScaleUnitId,
            };
    }
}
