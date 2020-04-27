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
        
        public UserProviderMiddleware(RequestDelegate next) { _next = next;}

        public async Task Invoke(HttpContext httpContext, IUserProvider userProvider, ILanguageProvider languageProvider)
        {
            userProvider.SetUser(httpContext.User, languageProvider.SelectedLanguage.Id);

            //var controllerName = GetControllerNamefromPath(httpContext.Request.Path.Value);
            //var ActionName = GetActionNamefromPath(httpContext.Request.Path.Value);

            //if (GetAuthorizedController(controllerName,ActionName)) {

            //    if (httpContext.User.Identity.IsAuthenticated)
            //    {
            //        if (Authorize(httpContext, controllerName, ActionName)) { await _next(httpContext); }
            //        else await UnauthorizedExceptionAsync(httpContext, " Un Authorize  Access");

            //    }
            //} else { await _next(httpContext); }

            await _next(httpContext);

            //else if (controllerName=="Auth"&& GetActionName(httpContext.Request.Path.Value)=="Login") { await _next(httpContext); }
            //else { await UnauthorizedExceptionAsync(httpContext, " Access Denay"); }







            //var endpoint = context.GetEndpoint();
            //if (endpoint != null)
            //{
            //    var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            //    if (controllerActionDescriptor != null)
            //    {
            //        var controllerName = controllerActionDescriptor.ControllerName;
            //        // you can log controllerName here
            //    }
            //}


        }

        //public string getControllerNameFromUrl()
        //{
        //    RouteCollection rc = new RouteCollection();
        //    MvcApplication.RegisterRoutes(rc);
        //    System.Web.Routing.RouteData rd = new RouteData();
        //    var context = new FakeHttpContext("\\" + HttpContext.Request.Url.AbsolutePath);
        //    rd = rc.GetRouteData(context);
        //    return rd.Values["action"].ToString();
        //}
        private bool GetAuthorizedController( string controllerName, string ActionName)
        {
            using (RealEstateDbContext _dbContext = new RealEstateDbContext())
            {
                var find =  _dbContext.Apicontroller.Where(item =>
                                         item.ControllerName.Trim().ToLower() == controllerName.Trim().ToLower() &&
                                         item.ActionName.Trim().ToLower() == ActionName.Trim().ToLower() ).ToList();
               
               return find.Count > 0?true:false;
             
            };
        }
        private bool Authorize(HttpContext httpContext,string controllerName,string ActionName)
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
                                    ActionName=d.ActionName,
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
                        if ((item.ActionReadPermmit && item.RoleReadPermission)||
                         (item.ActionDeletePermmit && item.RoleDeletePermission) ||
                         (item.ActionUpdatePermmit && item.RoleUpdatePermission)
                        )                           
                        {
                            return  true;
                        }
                    }
                };

            return false;
        }
        private string GetControllerNamefromPath(string Path)
        {
            if (Path.StartsWith("/api"))
            { 
                var text1 = Path.Substring(5);
            var index = text1.IndexOf("/");
            return index==-1? text1 : text1.Substring(0, index);

            }else 
                {
               
                return Path;
            }
           
           
        }

        private string GetActionNamefromPath(string Path)
        {
            var text1 = Path.Substring(5);
            var index = text1.IndexOf('/');

            return index == -1 ? "" : text1.Substring(index + 1);
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
            var result = JsonConvert.SerializeObject(new { Message = Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
       
            return context.Response.WriteAsync(result);
        }
    }
}
