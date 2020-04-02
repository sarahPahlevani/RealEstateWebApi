using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Estate
{
    public class PropertyPriceDto : ModelDtoBase<PropertyPrice>
    {
        public override int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public int PriceScaleUnitId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string BeforePriceLabel { get; set; }
        public string AfterPriceLabel { get; set; }

        [Required]
        public bool PriceOnCall { get; set; }
        [Required]
        public decimal CalculatedPriceUnit { get; set; }

        public override IModelDto<PropertyPrice> From(PropertyPrice entity)
        {
            Id = entity.Id;
            PropertyId = entity.Id;
            CurrencyId = entity.CurrencyId;
            PriceScaleUnitId = entity.PriceScaleUnitId;
            Price = entity.Price;
            BeforePriceLabel = entity.BeforePriceLabel;
            AfterPriceLabel = entity.AfterPriceLabel;
            PriceOnCall = entity.PriceOnCall;
            CalculatedPriceUnit = entity.CalculatedPriceUnit;
            return this;
        }

        public override PropertyPrice Create() =>
            new PropertyPrice
            {
                Id = PropertyId,
                CurrencyId = CurrencyId,
                PriceScaleUnitId = PriceScaleUnitId,
                Price = Price,
                BeforePriceLabel = BeforePriceLabel,
                AfterPriceLabel = AfterPriceLabel,
                PriceOnCall = PriceOnCall,
                CalculatedPriceUnit = CalculatedPriceUnit,
            };

        public override PropertyPrice Update() =>
            new PropertyPrice
            {
                Id = PropertyId,
                CurrencyId = CurrencyId,
                PriceScaleUnitId = PriceScaleUnitId,
                Price = Price,
                BeforePriceLabel = BeforePriceLabel,
                AfterPriceLabel = AfterPriceLabel,
                PriceOnCall = PriceOnCall,
                CalculatedPriceUnit = CalculatedPriceUnit,
            };
    }
}
