using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class CurrencyDto : ModelDtoBase<Currency>
    {
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        public override IModelDto<Currency> From(Currency entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Code = entity.Code;
            Symbol = entity.Symbol;
            IsDefault = entity.IsDefault;
            return this;
        }

        public override Currency Create() =>
            new Currency
            {
                Name = Name,
                Code = Code,
                IsDefault = IsDefault,
                Symbol = Symbol,
            };

        public override Currency Update() =>
            new Currency
            {
                Id = Id,
                Name = Name,
                Code = Code,
                IsDefault = IsDefault,
                Symbol = Symbol,
            };
    }
}
