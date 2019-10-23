using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Organization;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Organization
{
    public class RealEstateController : ModelController<RealEstate, RealEstateDto>
    {
        public RealEstateController(IModelService<RealEstate, RealEstateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<RealEstate>, IQueryable<RealEstateDto>> DtoConverter
        => items => items.Select(i => new RealEstateDto
        {
            LanguageIdDefault = i.LanguageIdDefault,
            Name = i.Name,
            Address01 = i.Address01,
            Address02 = i.Address02,
            Phone01 = i.Phone01,
            Phone02 = i.Phone02,
            Phone03 = i.Phone03,
            Fax = i.Fax,
            ZipCode = i.ZipCode,
            Email = i.Email,
            WebsiteUrl = i.WebsiteUrl,
            MetadataJson = i.MetadataJson,
            Id = i.Id,
        });
    }
}
