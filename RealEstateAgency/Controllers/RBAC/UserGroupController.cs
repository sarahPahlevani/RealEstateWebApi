using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Implementations.ActionFilters;

namespace RealEstateAgency.Controllers.RBAC
{
    public class UserGroupController : ModelController<UserGroup, UserGroupDto>
    {
        public UserGroupController(IModelService<UserGroup, UserGroupDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<UserGroup>, IQueryable<UserGroupDto>> DtoConverter =>
            items => items.Select(i => new UserGroupDto
            {
                Id = i.Id,
                Name = i.Name,
                StaticCode = i.StaticCode
            });
        // [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
       
        [HttpGet("[Action]")]
        public async Task<ActionResult<IEnumerable<UserGroupDto>>> GetRole(CancellationToken cancellationToken)
      => await ModelService.DbContext.UserGroup.Select(i => new UserGroupDto
      {
          Id = i.Id,
          Name = i.Name
          
      }).ToListAsync(cancellationToken);
    }
}
