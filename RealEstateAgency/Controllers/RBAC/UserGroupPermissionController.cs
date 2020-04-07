﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IEntityService<UserGroupPermission> _usergrouppermission;

        public UserGroupPermissionController(IModelService<UserGroupPermission, UserGroupPermissionDto> modelService, 
            IEntityService<UserGroupPermission> usergrouppermission,
            IEntityService<RealEstate> entityService) : base(modelService)
        {
            _usergrouppermission = usergrouppermission;
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

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        [HttpPut("[Action]")]
        public async Task<ActionResult> UpdateUserGroupPermission([FromBody]UserGroupPermissionDto dto, CancellationToken cancellationToken)
        {
            var user = await ModelService.GetAsync(u => u.Id == dto.Id, cancellationToken);
            user.MenuId = dto.MenuId;
            user.ReadPermission = dto.ReadPermission;
           
            user.UpdatePermission = dto.UpdatePermission;
            user.ReadPermission = dto.ReadPermission;
            user.UserGroupId = dto.UserGroupId;
          
            await ModelService.UpdateAsync(user, cancellationToken);

            return NoContent();
        }
        
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

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpGet("[Action]/{id}")]
        public async Task<ActionResult<UserGroupPermissionDto>> GetById(int id, CancellationToken cancellationToken)
      => await ModelService.DbContext.UserGroupPermission.Where(item => item.Id == id).Select(i => new UserGroupPermissionDto
      {
          Id = i.Id,
          UserGroupId = i.UserGroupId,
          MenuId = i.MenuId,
          DeletePermission = i.DeletePermission,
          UpdatePermission = i.UpdatePermission,
          ReadPermission = i.ReadPermission
      }).FirstOrDefaultAsync(cancellationToken);
       

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpDelete("[Action]/{id}")]
        public override async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return await base.Delete(id, cancellationToken);
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpGet("[Action]")]
        public async Task<ActionResult<IEnumerable<UserPermission>>> GetUserGroupPermission(CancellationToken cancellationToken)
       => await ModelService.DbContext.UserGroupPermission.Include(item=> item.Menu).Include(item=> item.UserGroup).Select(i => new UserPermission
       {
           Id = i.Id,
           Menu = i.Menu.Name,
           MenuId = i.MenuId,
           RoleId=i.UserGroupId,
           Role=i.UserGroup.Name,
           HasDeletePermmite = i.DeletePermission,
           HasUpdatePermmite = i.UpdatePermission,
           HasReadPermmite = i.ReadPermission
       }).ToListAsync(cancellationToken);
    }
}