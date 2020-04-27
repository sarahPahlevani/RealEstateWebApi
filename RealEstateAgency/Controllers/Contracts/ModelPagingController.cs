using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.Implementations.ActionFilters;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.Contracts;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Controllers.Contracts
{
    [Route("api/[controller]"), ApiController, ServiceFilter(typeof(ExecutionActionFilter)), ServiceFilter(typeof(AuthorizeActionFilter))]
    public abstract class ModelPagingController<TEntity, TDto, TPaginationDto> : ModelController<TEntity, TDto>
        where TEntity : class, IEntity
        where TDto : class, IModelDto<TEntity>, new()
        where TPaginationDto : class, IDto
    {
        protected ModelPagingController(IModelService<TEntity, TDto> modelService) : base(modelService)
        {
        }

        public abstract Func<IQueryable<TEntity>, IQueryable<TPaginationDto>> PagingConverter { get; }

        [HttpGet("page/{pageSize}/{pageNumber}")]
        public virtual async Task<ActionResult<PageResultDto<TPaginationDto>>> GetPageAsync(int pageSize, int pageNumber,
            CancellationToken cancellationToken) =>
            await GetPageResultAsync(ModelService.Queryable, new PageRequestDto(pageSize, pageNumber), new NullFilter<TEntity>(), cancellationToken);

        [HttpPost("page")]
        public virtual async Task<ActionResult<PageResultDto<TPaginationDto>>> GetPageAsync(
            [FromBody] PageRequestFilterDto requestDto
            , CancellationToken cancellationToken) =>
            await GetPageResultAsync(ModelService.Queryable, requestDto, new NullFilter<TEntity>(), cancellationToken);

        protected async Task<ActionResult<PageResultDto<TPaginationDto>>> GetPageResultAsync<TPageFilter>(IQueryable<TEntity> itemsQuery, PageRequestDto requestDto, TPageFilter pageFilter, CancellationToken cancellationToken)
            where TPageFilter :
            class, IPageFilter<TEntity>, new() =>
            await new PageResultDto<TPaginationDto>(PagingConverter(pageFilter.Filter(itemsQuery)), requestDto
                ).GetPage(cancellationToken);
    }
}
