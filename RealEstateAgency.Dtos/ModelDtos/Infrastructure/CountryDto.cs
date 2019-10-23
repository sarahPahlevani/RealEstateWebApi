using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class CountryDto : ModelDtoBase<Country>
    {
        public override int Id { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public string Name { get; set; }

        public string OfficialShortForm { get; set; }
        public string OfficialLongForm { get; set; }
        public int? Isocode { get; set; }
        public string Isoshort { get; set; }
        public string Isolong { get; set; }

        public override IModelDto<Country> From(Country entity)
        {
            Id = entity.Id;
            CurrencyId = entity.CurrencyId;
            Name = entity.Name;
            OfficialShortForm = entity.OfficialShortForm;
            OfficialLongForm = entity.OfficialLongForm;
            Isocode = entity.Isocode;
            Isoshort = entity.Isoshort;
            Isolong = entity.Isolong;
            return this;
        }

        public override Country Create() =>
            new Country
            {
                CurrencyId = CurrencyId,
                Name = Name,
                OfficialShortForm = OfficialShortForm,
                OfficialLongForm = OfficialLongForm,
                Isocode = Isocode,
                Isoshort = Isoshort,
                Isolong = Isolong,
            };

        public override Country Update() =>
            new Country
            {
                Id = Id,
                CurrencyId = CurrencyId,
                Name = Name,
                OfficialShortForm = OfficialShortForm,
                OfficialLongForm = OfficialLongForm,
                Isocode = Isocode,
                Isoshort = Isoshort,
                Isolong = Isolong,
            };
    }
}
