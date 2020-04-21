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

namespace RealEstateAgency.Controllers.RBAC
{

    public class UserGroupPermissionController : ModelPagingController<UserGroupPermission, UserGroupPermissionDto, UserPermissionList>
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

        //public override Func<IQueryable<UserGroupPermission>, IQueryable<UserGroupPermissionDto>> PagingConverter=> items => items.Select(i => new UserGroupPermissionDto
        //{
        //    Id = i.Id,
        //    UserGroupId =i. UserGroupId,
        //    MenuId = i.MenuId,
        //    DeletePermission = i.DeletePermission,
        //    UpdatePermission = i.UpdatePermission,
        //    ReadPermission = i.ReadPermission
        //});

        public override Func<IQueryable<UserGroupPermission>, IQueryable<UserGroupPermissionDto>> DtoConverter => items => items.Select(i => new UserGroupPermissionDto
        {
            Id = i.Id,
            UserGroupId = i.UserGroupId,
            MenuId = i.MenuId,
            DeletePermission = i.DeletePermission,
            UpdatePermission = i.UpdatePermission,
            ReadPermission = i.ReadPermission
        });

        public override Func<IQueryable<UserGroupPermission>, IQueryable<UserPermissionList>>  PagingConverter => items => items.Include(i => i.Menu).Include(i => i.UserGroup).Select(i => new UserPermissionList
        {
            Id = i.Id,
            Menu=i.Menu.Name,
            Role=i.UserGroup.Name,
            RoleId = i.UserGroupId,
            MenuId = i.MenuId,
            HasDeletePermmite = i.DeletePermission,
            HasUpdatePermmite = i.UpdatePermission,
            HasReadPermmite = i.ReadPermission
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

        public override async Task<ActionResult<PageResultDto<UserPermissionList>>> GetPageAsync(
          [FromBody] PageRequestFilterDto requestDto, CancellationToken cancellationToken) =>
          await GetPageResultAsync(ModelService.Queryable,
              requestDto, requestDto.Filter.ToObject<UserGroupPermissionListFilter>(),
              cancellationToken);

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        public override Task<ActionResult<PageResultDto<UserPermissionList>>>
            GetPageAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
            => base.GetPageAsync(pageSize, pageNumber, cancellationToken);


        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpDelete("[Action]/{id}")]
        public override async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return await base.Delete(id, cancellationToken);
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpGet("[Action]")]
        public async Task<ActionResult<IEnumerable<UserPermission>>> GetUserGroupPermission(CancellationToken cancellationToken)
       => await ModelService.DbContext.UserGroupPermission.Include(item=> item.Menu).Include(item=> item.UserGroup).Where(item=> item.Menu.ParentId!=null).Select(i => new UserPermission
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