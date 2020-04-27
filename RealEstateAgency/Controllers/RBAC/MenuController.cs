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
using RealEstateAgency.Implementations.Providers;
using RealEstateAgency.Implementations.Extensions;
using RealEstateAgency.Implementations.ActionFilters;

namespace RealEstateAgency.Controllers.RBAC
{

    public class MenuController : ModelPagingController<Menu, MenuDto, MenuDto>
    {
        
        private readonly IEntityService<RealEstate> _entityService;
        private readonly ILanguageProvider _languageProvider;

        public MenuController(IModelService<Menu, MenuDto> modelService,
            IEntityService<RealEstate> entityService, ILanguageProvider languageProvider) : base(modelService)
        {
            _languageProvider = languageProvider;
            _entityService = entityService;
         }

        public override Func<IQueryable<Menu>, IQueryable<MenuDto>> PagingConverter=> items => items.Include(i => i.UserGroupPermission)
        .Select(i => new MenuDto
        {
            Id = i.Id,
            Name = i.Name,
            ActionName = i.ActionName,
            ControllerName = i.ControllerName,
            PluginName = i.PluginName,
            IconName = i.IconName
        });

        public override Func<IQueryable<Menu>, IQueryable<MenuDto>> DtoConverter => items => items.Include(i=> i.UserGroupPermission)
        
        
        .Select(i => new MenuDto
        {
            Id = i.Id,
            Name = i.Name,
            ActionName = i.ActionName,
            ControllerName = i.ControllerName,
            PluginName = i.PluginName,
            IconName = i.IconName
        });

       // [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpGet("[Action]")]
        [AuthorizeActionFilter]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetAllPanelMenu(CancellationToken cancellationToken)
       => await ModelService.DbContext.Menu.Where(t => t.IsPanelPage == true).Select(i => new MenuDto
       {
           Id = i.Id,
           Name = i.Name,
           ActionName = i.ActionName,
           ControllerName = i.ControllerName,
           PluginName = i.PluginName,
           IconName = i.IconName
       }).ToListAsync(cancellationToken);


        [HttpGet("[Action]")]
        [AuthorizeActionFilter]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenuPermission12(CancellationToken cancellationToken)
            =>
            await ModelService.DbContext.Menu.Include(i => i.UserGroupPermission.Where(item => item.UserGroupId == 1))

                .Select(i => new MenuDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    ActionName = i.ActionName,
                    ControllerName = i.ControllerName,
                    PluginName = i.PluginName,
                    IconName = i.IconName
                }).ToListAsync(cancellationToken);

        //[Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpGet("[Action]")]
        [AuthorizeActionFilter]
        public ActionResult<IEnumerable<Menu>> GetAllMenu(CancellationToken cancellationToken)
        {
          
            var item = _entityService.DbContext.Menu.Where(t => t.IsPanelPage == true).ToList();

            return item;


        }

       // [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpGet("[Action]")]
        [AuthorizeActionFilter]
        public async Task<ActionResult<IEnumerable<MenuNameDto>>> GetMenu(CancellationToken cancellationToken)
        {
            var item1 = await ModelService.DbContext.Menu.Select(i => new MenuNameDto
                   {
                       Id = i.Id,
                MenuTypeName = i.Name
            }).ToListAsync(cancellationToken);
            return item1;
        }

        


        //[Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpGet("[Action]/{id}")]
        [AuthorizeActionFilter]
        public async Task<ActionResult<IEnumerable<ParentMenuDto>>> GetMenuPermission(CancellationToken cancellationToken,int id) 
            => await ModelService.DbContext.UserGroupPermission.Where(t => t.UserGroupId==id)
                   .Include(i => i.Menu).Where(f=>f.Menu.IsPanelPage==true && f.Menu.ParentId==null && f.Menu.HasMenu)
            .Select ( g => new ParentMenuDto
                   {
                       Id = g.Id,
                       Name = g.Menu.Name,
                       ActionName = g.Menu.ActionName,
                       ControllerName = g.Menu.ControllerName,
                       PluginName = g.Menu.PluginName,
                       IconName = g.Menu.IconName,
                       subs = ModelService.DbContext.Menu.Where(t => t.ParentId == g.MenuId).Select(ff => new subMenuDto
                       {
                           Id = ff.Id,
                           Name = ff.Name,
                           ActionName = ff.ActionName,
                           ControllerName = ff.ControllerName,
                           PluginName = ff.PluginName,
                           IconName = ff.IconName,

                       }).ToList()

                   }).ToListAsync(cancellationToken);
        
        [HttpGet("[Action]/{language}")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetAllByLanguage(string language, CancellationToken cancellationToken)
        {
            try
            {
                var lang = _languageProvider[language];
                return await DtoConverter(ModelService.DbContext.Menu.Translate(lang.Id)).ToListAsync(cancellationToken);
            }
            catch (KeyNotFoundException)
            {
                return await base.GetAllAsync(cancellationToken);
            }
        }
    }
}