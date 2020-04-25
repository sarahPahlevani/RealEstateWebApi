using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RealEstateAgency.Shared.Statics;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters;
using RealEstateAgency.Implementations.Providers;
using RealEstateAgency.Implementations.Extensions;

namespace RealEstateAgency.Controllers.RBAC
{

    public class ContentPageController : ModelPagingController<ContentPage, ContentPageDto, ContentPageList>
    {
        private readonly ILanguageProvider _languageProvider;
        public ContentPageController(IModelService<ContentPage, ContentPageDto> modelService, ILanguageProvider languageProvider)
            : base(modelService)
        {
            _languageProvider = languageProvider;
        }
     
        public override Func<IQueryable<ContentPage>, IQueryable<ContentPageDto>> DtoConverter => items => items.Select(i => new ContentPageDto
        {
          
            Id = i.Id,
            ContentHeader = i.ContentHeader,
            ContectDetail = i.ContectDetail,
            ContentFooter = i.ContentFooter,
            Title = i.Title,
            MenuId = i.MenuId
        });

        public override Func<IQueryable<ContentPage>, IQueryable<ContentPageList>>  PagingConverter => items => items.Select(i => new ContentPageList
        {
            Id = i.Id,
            ContentHeader = i.ContentHeader,
            ContectDetail = i.ContectDetail,
            ContentFooter = i.ContentFooter,
            Title = i.Title,
            MenuId = i.MenuId
        });

        [HttpPut("[Action]")]
        public async Task<ActionResult> UpdateContentPage([FromBody]ContentPageDto dto, CancellationToken cancellationToken)
        {
            var user = await ModelService.GetAsync(u => u.Id == dto.Id, cancellationToken);
            user.MenuId = dto.MenuId;
            user.ContentHeader = dto.ContentHeader;
           
            user.ContentFooter = dto.ContentFooter;
            user.ContectDetail = dto.ContectDetail;
            user.Title = dto.Title;
          
            await ModelService.UpdateAsync(user, cancellationToken);

            return NoContent();
        }
        
       
     
        [HttpGet("[Action]/{id}")]
        public async Task<ActionResult<ContentPage>> GetById(int id, CancellationToken cancellationToken)
      => await ModelService.DbContext.ContentPage.Where(item => item.Id == id).Select(i => new ContentPage
      {
          Id = i.Id,
          ContentHeader = i.ContentHeader,
          ContectDetail = i.ContectDetail,
          ContentFooter = i.ContentFooter,
          Title = i.Title,
          MenuId = i.MenuId
      }).FirstOrDefaultAsync(cancellationToken);

        public override async Task<ActionResult<PageResultDto<ContentPageList>>> GetPageAsync(
          [FromBody]PageRequestFilterDto requestDto, CancellationToken cancellationToken) =>
          await GetPageResultAsync(ModelService.Queryable,
              requestDto, requestDto.Filter.ToObject<ContentPageListFilter>(),
              cancellationToken);

       
        public override Task<ActionResult<PageResultDto<ContentPageList>>>
            GetPageAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
            => base.GetPageAsync(pageSize, pageNumber, cancellationToken);


      
        [HttpDelete("[Action]/{id}")]
        public override async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return await base.Delete(id, cancellationToken);
        }
        [HttpGet("[Action]")]
        public async Task<ActionResult<IEnumerable<ContentPage2>>> GetPageContent(CancellationToken cancellationToken)
     => await ModelService.DbContext.ContentPage.Select(i => new ContentPage2
     {
         Id = i.Id,
         ContentHeader = i.ContentHeader,
         ContectDetail = i.ContectDetail,
         ContentFooter = i.ContentFooter,
         Title = i.Title,
       
     }).ToListAsync(cancellationToken);
        [AllowAnonymous]
        [HttpGet("[Action]/{language}")]
        public async Task<ActionResult<IEnumerable<ContentPageDto>>> GetAllByLanguage(string language, CancellationToken cancellationToken)
        {
            try
            {
                var lang = _languageProvider[language];
                return await DtoConverter(ModelService.DbContext.ContentPage.Translate(lang.Id)).ToListAsync(cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                return await base.GetAllAsync(cancellationToken);
            }
        }
    }
}