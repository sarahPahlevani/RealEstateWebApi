using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class LanguageDto : ModelDtoBase<Language>
    {
        public override int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Type { get; set; }

        public bool IsDefault { get; set; }
        public string UrlCode { get; set; }

        public override IModelDto<Language> From(Language entity)
        {
            Id = entity.Id;
            Code = entity.Code;
            IsDefault = entity.IsDefault;
            Type = entity.Type;
            return this;
        }

        public override Language Create() =>
            new Language
            {
                Code = Code,
                IsDefault = IsDefault,
                Type = Type,
            };

        public override Language Update() =>
            new Language
            {
                Id = Id,
                Code = Code,
                IsDefault = IsDefault,
                Type = Type,
            };
    }
}
