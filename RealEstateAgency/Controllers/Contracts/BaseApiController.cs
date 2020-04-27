using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Implementations.ActionFilters;

namespace RealEstateAgency.Controllers.Contracts
{
    [ Authorize, Route("api/[controller]"), ApiController, ServiceFilter(typeof(ExecutionActionFilter)), ServiceFilter(typeof(AuthorizeActionFilter))]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
