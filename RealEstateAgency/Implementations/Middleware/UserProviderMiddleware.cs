using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.Providers;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.DAL.Models;
using Microsoft.AspNetCore.Routing;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;
using System.Net;
using System;
using Newtonsoft.Json;

namespace RealEstateAgency.Implementations.Middleware
{
    public class UserProviderMiddleware
    {
        private readonly RequestDelegate _next;
        
        public UserProviderMiddleware(RequestDelegate next//, IEntityService<RealEstate> entityService
            ) { _next = next;// _entityService = entityService; 
        }

        public async Task Invoke(HttpContext httpContext, IUserProvider userProvider, ILanguageProvider languageProvider)
        {
            userProvider.SetUser(httpContext.User, languageProvider.SelectedLanguage.Id);

            var controllerName = GetControllerName(httpContext.Request.Path.Value);
            if (Check(httpContext, controllerName)) { await _next(httpContext); } 
            
            else if (httpContext.User.Identity.IsAuthenticated) { 
        

                if (Authorize(httpContext, controllerName)) { await _next(httpContext); }
                else await UnauthorizedExceptionAsync(httpContext, " Un Authorize  Access");

            }
          
            else { await UnauthorizedExceptionAsync(httpContext, " Access Denay"); }
                       
           
        }
        private bool Check(HttpContext httpContext, string controllerName)
        {
            using (RealEstateDbContext _dbContext = new RealEstateDbContext())
            {
                var find =  _dbContext.Apicontroller.Where(item=>item.AllAccess==true).ToList();
               
                foreach (var item in find)
                {
                    if (item.ControllerName.ToLower() == controllerName.ToLower() )
                    {
                        return true;
                    }
                }
            };

            return false;
        }
        private bool Authorize(HttpContext httpContext,string controllerName)
        {
                     
                var climerole = httpContext.User.Claims.Where(item => item.Type == ClaimTypes.Role).Select(i => i.Value).FirstOrDefault();
               
                
                using (RealEstateDbContext _dbContext = new RealEstateDbContext())
                {
                    var find = (from a in _dbContext.Menu
                                join b in _dbContext.UserGroupPermission on a.Id equals b.MenuId
                                join c in _dbContext.UserGroup on b.UserGroupId equals c.Id
                                join d in _dbContext.Apicontroller on a.Id equals d.MenuId
                                where c.Name == climerole// && d.UserAccountId.ToString() == httpContext.User.Identity.Name
                                select new
                                {
                                    MenuName = a.Name,
                                    ControllerName = d.ControllerName,
                                    ActionName = a.ActionName,
                                    PluginName = a.PluginName,
                                    ReadPermission = b.ReadPermission,
                                    UpdatePermission = b.UpdatePermission,
                                    DeletePermission = b.DeletePermission,
                                    UserGroupName = c.Name

                                }).ToList();
                    foreach (var item in find)
                    {
                        if (item.ControllerName.ToLower() == controllerName.ToLower() &&
                            (checkPermmite(httpContext.Request.Method, item.ReadPermission, item.DeletePermission, item.UpdatePermission)))
                        {
                            return  true;
                        }
                    }
                };

            return false;
        }
        private string GetControllerName(string Path)
        {
            var text1 = Path.Substring(5);
            var index = text1.IndexOf("/");
            return index==-1? text1 : text1.Substring(0, index);
        }
        private bool checkPermmite(string MethodType, bool read, bool delete, bool update)
        {
            return hasReadPermmite(MethodType, read) || hasDeletePermmite(MethodType, delete) || hasUpdatePermmite(MethodType, update);
        }
        private bool hasReadPermmite(string MethodType, bool read)
        {
            return MethodType == "GET" && read == true;
        }
        private bool hasDeletePermmite(string MethodType, bool delete)
        {
            return MethodType == "DELETE" && delete == true;
        }

        private bool hasUpdatePermmite(string MethodType, bool update)
        {
            return (MethodType == "POST" || MethodType == "PUT" )&& update == true;
        }
        private static Task UnauthorizedExceptionAsync(HttpContext context, string Message)
        {
            var code =  HttpStatusCode.Unauthorized;
            var result = JsonConvert.SerializeObject(new { error = Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
