using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.DAL.Contracts;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.Implementations.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;

namespace RealEstateAgency.Controllers.Contracts
{
    [Route("api/[controller]"), ApiController, ServiceFilter(typeof(ExecutionActionFilter))]
    public abstract class ModelController<TEntity, TDto> : BaseApiController
        where TEntity : class, IEntity
        where TDto : class, IModelDto<TEntity>, new()
    {
        protected readonly IModelService<TEntity, TDto> ModelService;

        protected ModelController(IModelService<TEntity, TDto> modelService)
        {
            ModelService = modelService;
            ModelService.SetQuery(DtoConverter);
        }

        public abstract Func<IQueryable<TEntity>, IQueryable<TDto>> DtoConverter { get; }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var res = await ModelService.GetAllDtosAsync(cancellationToken);
            return new ActionResult<IEnumerable<TDto>>(res);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDto>> GetAsync(int id, CancellationToken cancellationToken)
            => HandleGetResult(await ModelService.GetDtoAsync(id, cancellationToken));

        [HttpPost]
        public virtual async Task<ActionResult<TDto>> Create([FromBody] TDto value, CancellationToken cancellationToken)
            => await ModelService.CreateByDtoAsync(value, cancellationToken);

        [HttpPut]
        public virtual async Task<ActionResult> UpdateAsync([FromBody] TDto value, CancellationToken cancellationToken)
        {
            await ModelService.UpdateByDtoAsync(value, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await ModelService.DeleteByIdAsync(id, cancellationToken);
            return NoContent();
        }

        protected ActionResult<TDto> HandleGetResult(TDto result)
        {
            if (result == null)
                return NotFound();

            return result;
        }
    }
}
