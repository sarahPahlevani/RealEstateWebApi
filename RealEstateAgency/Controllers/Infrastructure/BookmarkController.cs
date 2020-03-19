using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters;
using System.Collections.Generic;

namespace RealEstateAgency.Controllers.Infrastructure
{
    public class BookmarkController : ModelPagingController<Bookmark, BookmarkDto, BookmarkListDto>
    {
        private readonly IUserProvider _userProvider;

        public BookmarkController(IModelService<Bookmark, BookmarkDto> modelService, IUserProvider userProvider) : base(modelService)
        {
            _userProvider = userProvider;
        }

        public override Func<IQueryable<Bookmark>, IQueryable<BookmarkDto>> DtoConverter => _converter;
        public override Func<IQueryable<Bookmark>, IQueryable<BookmarkListDto>> PagingConverter => items => items
                .Select(i => new BookmarkListDto
                {
                    Id = i.Id,
                    UserAccountId = i.UserAccountId,
                    UserAccount = i.UserAccount,
                    PropertyId = i.PropertyId,
                    Property = i.Property,
                    DateCreated = i.DateCreated,
                }).OrderByDescending(i => i.DateCreated);

        private Func<IQueryable<Bookmark>, IQueryable<BookmarkDto>> _converter =>
            entities => entities.Select(i =>
                new BookmarkDto
                {
                    Id = i.Id,
                    UserAccountId = i.UserAccountId,
                    PropertyId = i.PropertyId,
                    DateCreated = i.DateCreated,
                });

        [HttpGet("[Action]")]
        public ActionResult<List<BookmarkListDto>> GetByUser()
        {
            return ModelService.Queryable.Where(r => r.UserAccountId == _userProvider.Id)
                .Select(r => new BookmarkListDto
                {
                    Id = r.Id,
                    UserAccountId = r.UserAccountId,
                    UserAccount = r.UserAccount,
                    PropertyId = r.PropertyId,
                    Property = r.Property,
                    DateCreated = r.DateCreated,
                }).OrderByDescending(r => r.DateCreated).ToList();
        }
    }
}
