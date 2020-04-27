using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAgency.Implementations.ActionFilters
{
    public class AuthorizeActionFilter : Attribute, IActionFilter, IFilterMetadata, IAsyncActionFilter, IResultFilter, IAsyncResultFilter, IOrderedFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
