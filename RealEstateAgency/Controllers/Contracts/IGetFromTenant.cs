using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.DAL.DtoContracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateAgency.Controllers.Contracts
{
    internal interface IGetFromTenant<TDto>
    where TDto : class, IDto
    {
        Task<ActionResult<IEnumerable<TDto>>> GetFromTenant(int tenantId,
            CancellationToken cancellationToken);
    }
}
