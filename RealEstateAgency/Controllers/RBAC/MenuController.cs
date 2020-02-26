using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.RBAC
{

    public class MenuController// : ModelPagingController<Menu, MenuDto, MenuDto>
    {
       
        public MenuController()
        {
        }

    //private Func<IQueryable<Menu>, IQueryable<MenuDto>> _convertor =>
    //    items => items.Select(i => new MenuDto
    //    {
    //        Id = i.Id,
    //        Name = i.Name,
    //        ActionName = i.ActionName,
    //        ControllerName = i.ControllerName,
    //        ParentId=i.ParentId,
    //        PluginName=i.PluginName,
    //        IsPanelPage = i.IsPanelPage
    //    });

    //public override Func<IQueryable<Menu>, IQueryable<MenuDto>> PagingConverter => _convertor;

    //    public override Func<IQueryable<Menu>, IQueryable<MenuDto>> DtoConverter => _convertor;
    }
}