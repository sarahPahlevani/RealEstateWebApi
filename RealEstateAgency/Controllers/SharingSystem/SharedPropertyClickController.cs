using System;
using System.Linq;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.SharingSystem;

namespace RealEstateAgency.Controllers.SharingSystem
{
    public class SharedPropertyClickController : ModelController<SharedPropertyClick, SharedPropertyClickDto>
    {
        public SharedPropertyClickController(IModelService<SharedPropertyClick, SharedPropertyClickDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<SharedPropertyClick>, IQueryable<SharedPropertyClickDto>> DtoConverter
            => items => items.Select(i => new SharedPropertyClickDto
            {
                Id = i.Id,
                SharedPropertyId = i.SharedPropertyId,
                MetaData = i.MetaData
            });
    }
}
