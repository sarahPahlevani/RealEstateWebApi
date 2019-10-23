using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Infrastructure
{
    public class CurrencyController : ModelPagingController<Currency, CurrencyDto, CurrencyDto>
    {
        public CurrencyController(IModelService<Currency, CurrencyDto> modelService) : base(modelService)
        {
        }

        private Func<IQueryable<Currency>, IQueryable<CurrencyDto>> _convertor =>
            items => items.Select(i => new CurrencyDto
            {
                Id = i.Id,
                Name = i.Name,
                IsDefault = i.IsDefault,
                Code = i.Code,
                Symbol = i.Symbol
            });

        public override Func<IQueryable<Currency>, IQueryable<CurrencyDto>> PagingConverter => _convertor;
        public override Func<IQueryable<Currency>, IQueryable<CurrencyDto>> DtoConverter => _convertor;
    }
}
