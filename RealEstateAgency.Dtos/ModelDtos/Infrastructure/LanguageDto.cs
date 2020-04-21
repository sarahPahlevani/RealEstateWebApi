using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class LanguageDto : ModelDtoBase<Language>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Type { get; set; }

        public string Flag { get; set; }

        public bool IsDefault { get; set; }

        public string UrlCode { get; set; }

        public override IModelDto<Language> From(Language entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Code = entity.Code;
            Type = entity.Type;
            Flag = entity.Flag;
            IsDefault = entity.IsDefault;
            return this;
        }

        public override Language Create() =>
            new Language
            {
                Name = Name,
                Code = Code,
                Type = Type,
                Flag = Flag,
                IsDefault = IsDefault,
            };

        public override Language Update() =>
            new Language
            {
                Id = Id,
                Name = Name,
                Code = Code,
                Type = Type,
                Flag = Flag,
                IsDefault = IsDefault,
            };
    }
}
