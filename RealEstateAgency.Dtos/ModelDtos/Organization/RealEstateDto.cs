using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.Organization
{
    public class RealEstateDto : ModelDtoBase<RealEstate>
    {
        public override int Id { get; set; }
        public int LanguageIdDefault { get; set; }
        public string Name { get; set; }
        public byte[] LogoPicture { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
        public string Phone01 { get; set; }
        public string Phone02 { get; set; }
        public string Phone03 { get; set; }
        public string Fax { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string WebsiteUrl { get; set; }
        public string MetadataJson { get; set; }
        public string DateFormat { get; set; }
        public int CurrencyId { get; set; }

        public override IModelDto<RealEstate> From(RealEstate entity)
        {
            Id = entity.Id;
            LanguageIdDefault = entity.LanguageIdDefault;
            Name = entity.Name;
            Address01 = entity.Address01;
            Address02 = entity.Address02;
            Phone01 = entity.Phone01;
            Phone02 = entity.Phone02;
            Phone03 = entity.Phone03;
            Fax = entity.Fax;
            ZipCode = entity.ZipCode;
            Email = entity.Email;
            WebsiteUrl = entity.WebsiteUrl;
            MetadataJson = entity.MetadataJson;
            DateFormat = entity.DateFormat;
            CurrencyId = entity.CurrencyId;
            return this;
        }

        public override RealEstate Create() =>
            new RealEstate
            {
                LanguageIdDefault = LanguageIdDefault,
                Name = Name,
                Address01 = Address01,
                Address02 = Address02,
                Phone01 = Phone01,
                Phone02 = Phone02,
                Phone03 = Phone03,
                Fax = Fax,
                ZipCode = ZipCode,
                Email = Email,
                WebsiteUrl = WebsiteUrl,
                MetadataJson = MetadataJson,
                CurrencyId = CurrencyId,
                DateFormat = DateFormat
            };

        public override RealEstate Update() =>
            new RealEstate
            {
                Id = Id,
                LanguageIdDefault = LanguageIdDefault,
                Name = Name,
                Address01 = Address01,
                Address02 = Address02,
                Phone01 = Phone01,
                Phone02 = Phone02,
                Phone03 = Phone03,
                Fax = Fax,
                ZipCode = ZipCode,
                Email = Email,
                WebsiteUrl = WebsiteUrl,
                MetadataJson = MetadataJson,
                CurrencyId = CurrencyId,
                DateFormat = DateFormat
            };
    }
}
