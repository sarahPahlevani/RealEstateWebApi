using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.Providers;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.DAL.Models;
using Microsoft.AspNetCore.Routing;

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

          
            if (httpContext.User.Identity.IsAuthenticated)
            {
               
                if (Authorize(httpContext)) { await _next(httpContext); } 
                else await _next(null);

            }
            else {
                await _next(httpContext);
            }
                       
           
        }
        private bool Authorize(HttpContext httpContext)
        {
                     
                var climerole = httpContext.User.Claims.Where(item => item.Type == ClaimTypes.Role).Select(i => i.Value).FirstOrDefault();
               
                
                using (RealEstateDbContext _dbContext = new RealEstateDbContext())
                {
                    var find = (from a in _dbContext.Menu
                                join b in _dbContext.UserGroupPermission on a.Id equals b.MenuId
                                join c in _dbContext.UserGroup on b.UserGroupId equals c.Id
                              //  join d in _dbContext.UserAccountGroup on c.Id equals d.UserGroupId
                                where c.Name == climerole// && d.UserAccountId.ToString() == httpContext.User.Identity.Name
                                select new
                                {
                                    MenuName = a.Name,
                                    ControllerName = a.ApicontrollerName,
                                    ActionName = a.ActionName,
                                    PluginName = a.PluginName,
                                    ReadPermission = b.ReadPermission,
                                    UpdatePermission = b.UpdatePermission,
                                    DeletePermission = b.DeletePermission,
                                    UserGroupName = c.Name

                                }).ToList();
                    foreach (var item in find)
                    {
                        if (item.ControllerName == GetControllerName(httpContext.Request.Path.Value) &&
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
            return text1.Substring(0, index);
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
    }
}
