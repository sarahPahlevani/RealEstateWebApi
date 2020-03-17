using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.Infrastructure
{
    //public class BookmarkController : ModelPagingController<Bookmark, BookmarkDto, BookmarkDto>
    //{
    //    public BookmarkController(IModelService<Bookmark, BookmarkDto> modelService) : base(modelService)
    //    {
    //    }

    //    public override Func<IQueryable<Bookmark>, IQueryable<BookmarkDto>> DtoConverter => _converter;
    //    public override Func<IQueryable<Bookmark>, IQueryable<BookmarkDto>> PagingConverter => _converter;

    //    private Func<IQueryable<Bookmark>, IQueryable<BookmarkDto>> _converter =>
    //        entities => entities.Select(i =>
    //            new BookmarkDto
    //            {
    //                Id = i.Id,
    //                UserAccountId = i.UserAccountId,
    //                PropertyId = i.PropertyId,
    //                DateCreated = i.DateCreated,
    //            });
    //}
}
