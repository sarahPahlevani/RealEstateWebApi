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

namespace RealEstateAgency.Controllers.RBAC
{

    public class MenuController : ModelPagingController<Menu, MenuDto, MenuDto>
    {
        
        private readonly IEntityService<RealEstate> _entityService;
        

        public MenuController(IModelService<Menu, MenuDto> modelService,
            IEntityService<RealEstate> entityService) : base(modelService)
        {
           
            _entityService = entityService;
          
        }

        public override Func<IQueryable<Menu>, IQueryable<MenuDto>> PagingConverter=> items => items.Select(i => new MenuDto
        {
            Id = i.Id,
            Name = i.Name,
            ActionName = i.ActionName,
            ControllerName = i.ControllerName,
            PluginName = i.PluginName
        });

        public override Func<IQueryable<Menu>, IQueryable<MenuDto>> DtoConverter => items => items.Include(i=> i.UserGroupPermission)
        
        
        .Select(i => new MenuDto
        {
            Id = i.Id,
            Name = i.Name,
            ActionName = i.ActionName,
            ControllerName = i.ControllerName,
            PluginName = i.PluginName
        });

        [HttpGet("[Action]")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetAllMenu1(CancellationToken cancellationToken)
       => await ModelService.DbContext.Menu.Where(t => t.IsPanelPage == true).Select(i => new MenuDto
       {
           Id = i.Id,
           Name = i.Name,
           ActionName = i.ActionName,
           ControllerName = i.ControllerName,
           PluginName = i.PluginName
       }).ToListAsync(cancellationToken);


        [HttpGet("[Action]")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenuPermission12(CancellationToken cancellationToken)
            =>
            await ModelService.DbContext.Menu.Include(i => i.UserGroupPermission.Where(item => item.UserGroupId == 1))

                .Select(i => new MenuDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    ActionName = i.ActionName,
                    ControllerName = i.ControllerName,
                    PluginName = i.PluginName
                }).ToListAsync(cancellationToken);


        [HttpGet("[Action]")]
        public ActionResult<IEnumerable<Menu>> GetAllMenu(CancellationToken cancellationToken)
        {
          
            var item = _entityService.DbContext.Menu.Where(t => t.IsPanelPage == true).ToList();

            return item;


        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenuPermission1(CancellationToken cancellationToken)
        {
            var item1 =await _entityService.DbContext.Menu.Where(t => t.IsPanelPage == true).ToListAsync(cancellationToken);
            var tt=await ModelService.DbContext.Menu.Where(item => item.IsPanelPage == true).Select(i => new MenuDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    ActionName = i.ActionName,
                    ControllerName = i.ControllerName,
                    PluginName = i.PluginName
                }).ToListAsync(cancellationToken);
            var ite= await ModelService.DbContext.Menu.Include(i => i.UserGroupPermission.Where(item => item.UserGroupId == 1))

                .Select(i => new MenuDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    ActionName = i.ActionName,
                    ControllerName = i.ControllerName,
                    PluginName = i.PluginName
                }).ToListAsync(cancellationToken);
            return  tt;
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenuPermission(CancellationToken cancellationToken)
            =>
            await ModelService.DbContext.Menu.Where(item=>item.IsPanelPage == true)
                
                .Select(i => new MenuDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    ActionName=i.ActionName,
                    ControllerName=i.ControllerName,
                   PluginName=i.PluginName 
                }).ToListAsync(cancellationToken);
    }
}