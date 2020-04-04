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

namespace RealEstateAgency.Controllers.RBAC
{

    public class UserGroupPermissionController : ModelPagingController<UserGroupPermission, UserGroupPermissionDto, UserGroupPermissionDto>
    {
        
        private readonly IEntityService<RealEstate> _entityService;
        

        public UserGroupPermissionController(IModelService<UserGroupPermission, UserGroupPermissionDto> modelService,
            IEntityService<RealEstate> entityService) : base(modelService)
        {
           
            _entityService = entityService;
         }

        public override Func<IQueryable<UserGroupPermission>, IQueryable<UserGroupPermissionDto>> PagingConverter=> items => items.Select(i => new UserGroupPermissionDto
        {
            Id = i.Id,
            UserGroupId =i. UserGroupId,
            MenuId = i.MenuId,
            DeletePermission = i.DeletePermission,
            UpdatePermission = i.UpdatePermission,
            ReadPermission = i.ReadPermission
        });

        public override Func<IQueryable<UserGroupPermission>, IQueryable<UserGroupPermissionDto>> DtoConverter => items => items.Select(i => new UserGroupPermissionDto
        {
            Id = i.Id,
            UserGroupId = i.UserGroupId,
            MenuId = i.MenuId,
            DeletePermission = i.DeletePermission,
            UpdatePermission = i.UpdatePermission,
            ReadPermission = i.ReadPermission
        });

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpGet("[Action]")]
        public async Task<ActionResult<IEnumerable<UserGroupPermissionDto>>> GetAllUserGroupPermission(CancellationToken cancellationToken)
       => await ModelService.DbContext.UserGroupPermission.Select(i => new UserGroupPermissionDto
       {
           Id = i.Id,
           UserGroupId = i.UserGroupId,
           MenuId = i.MenuId,
           DeletePermission = i.DeletePermission,
           UpdatePermission = i.UpdatePermission,
           ReadPermission = i.ReadPermission
       }).ToListAsync(cancellationToken);


    }
}