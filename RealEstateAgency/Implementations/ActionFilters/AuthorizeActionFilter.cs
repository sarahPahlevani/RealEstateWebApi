using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using RealEstateAgency.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateAgency.Implementations.ActionFilters
{
    public class AuthorizeActionFilter : Attribute, IActionFilter
    {
        public bool isValid ;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.ActionDescriptor.RouteValues["controller"];
            var ActionName = context.ActionDescriptor.RouteValues["action"];

            if (GetAuthorizedController(controllerName,ActionName)) {

                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (!Authorize(context.HttpContext, controllerName, ActionName)) {
                        context.Result= new BadRequestObjectResult("Un Authorize  Access");
                        return;
                    }

                }
            }
           
        }
        
        private bool GetAuthorizedController(string controllerName, string ActionName)
        {
            using (RealEstateDbContext _dbContext = new RealEstateDbContext())
            {
                var find = _dbContext.Apicontroller.Where(item =>
                                        item.ControllerName.Trim().ToLower() == controllerName.Trim().ToLower() &&
                                        item.ActionName.Trim().ToLower() == ActionName.Trim().ToLower()).ToList();

                return find.Count > 0 ? true : false;

            };
        }
        private bool Authorize(HttpContext httpContext, string controllerName, string ActionName)
        {

            var climerole = httpContext.User.Claims.Where(item => item.Type == ClaimTypes.Role).Select(i => i.Value).FirstOrDefault();

            using (RealEstateDbContext _dbContext = new RealEstateDbContext())
            {
                var find = (from a in _dbContext.Menu
                            join b in _dbContext.UserGroupPermission on a.Id equals b.MenuId
                            join c in _dbContext.UserGroup on b.UserGroupId equals c.Id
                            join d in _dbContext.Apicontroller on a.Id equals d.MenuId
                            where c.Name == climerole && d.ControllerName.Trim().ToLower() == controllerName.Trim().ToLower()
                               && d.ActionName.Trim().ToLower() == ActionName.Trim().ToLower()
                            select new
                            {
                                MenuName = a.Name,
                                ControllerName = d.ControllerName,
                                ActionName = d.ActionName,
                                ActionReadPermmit = d.IsRead,
                                ActionDeletePermmit = d.IsDelete,
                                ActionUpdatePermmit = d.IsUpdate,
                                PluginName = a.PluginName,
                                RoleReadPermission = b.ReadPermission,
                                RoleUpdatePermission = b.UpdatePermission,
                                RoleDeletePermission = b.DeletePermission,
                                UserGroupName = c.Name

                            }).ToList();
                foreach (var item in find)
                {
                    if ((item.ActionReadPermmit && item.RoleReadPermission) ||
                     (item.ActionDeletePermmit && item.RoleDeletePermission) ||
                     (item.ActionUpdatePermmit && item.RoleUpdatePermission)
                    )
                    {
                        return true;
                    }
                }
            };

            return false;
        }
       
    }
}
