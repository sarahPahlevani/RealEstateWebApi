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
    public class UserProvider1Middleware
    {
        private readonly RequestDelegate _next;
        private readonly IEntityService<RealEstate> _entityService;
        public UserProvider1Middleware(RequestDelegate next//, IEntityService<RealEstate> entityService
            ) {
            
            _next = next;
            //_entityService = entityService;
        }

        public async Task Invoke(HttpContext httpContext, IUserProvider userProvider, ILanguageProvider languageProvider)
        {
            var user = httpContext.User;
            if (user.Identity.IsAuthenticated)
            {
            var find= _entityService.DbContext.UserGroupPermission.Where(i => i.UserGroup.Name == user.Identity.Name).ToList();
                bool isExist=false;
                var dd = httpContext.GetRouteData();
                var ss = httpContext.GetRouteValue("");
                foreach (var item in find)
                {
                    if (checkPermmite(httpContext.Request.Method, item.ReadPermission, item.DeletePermission, item.UpdatePermission))
                    {
                        isExist = true;
                    }
                }

            }
         //   userProvider.SetUser(httpContext.User, languageProvider.SelectedLanguage.Id);
            await _next(httpContext);
        }
        private bool checkPermmite(string MethodType,bool read,bool delete,bool update)
        {
            return hasReadPermmite(MethodType, read)|| hasDeletePermmite(MethodType, delete) || hasUpdatePermmite(MethodType, update);
        }
        private bool hasReadPermmite(string MethodType, bool read)
        {
            return MethodType == "Get" && read == true;
        }
        private bool hasDeletePermmite(string MethodType, bool delete)
        {
            return MethodType == "delete" && delete == true;
        }

        private bool hasUpdatePermmite(string MethodType, bool update)
        {
            return MethodType=="Post"&&update==true;
        }

    }
}
