using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class RegionTranslateDto : ModelDtoBase<RegionTranslate>
    {
        public override int Id { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        public override IModelDto<RegionTranslate> From(RegionTranslate entity)
        {
            Id = entity.Id;
            RegionId = entity.RegionId;
            LanguageId = entity.LanguageId;
            Name = entity.Name;
            return this;
        }

        public override RegionTranslate Create() =>
            new RegionTranslate
            {
                LanguageId = LanguageId,
                Name = Name,
                RegionId = RegionId,
            };

        public override RegionTranslate Update() =>
            new RegionTranslate
            {
                Id = Id,
                LanguageId = LanguageId,
                Name = Name,
                RegionId = RegionId,
            };
    }
}
