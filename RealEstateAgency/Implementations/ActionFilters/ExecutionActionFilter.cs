using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace RealEstateAgency.Implementations.ActionFilters
{
    public class ExecutionActionFilter : IActionFilter
    {
        private DateTime _started;

        public void OnActionExecuting(ActionExecutingContext context) => _started = DateTime.Now;

        public void OnActionExecuted(ActionExecutedContext context)
         {
            if (context.ModelState.IsValid)
                Console.WriteLine($"model validation completed in {(DateTime.Now - _started).TotalMilliseconds} Milliseconds");
        }
    }
}
