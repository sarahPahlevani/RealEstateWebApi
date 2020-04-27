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

           
            await _next(httpContext);




        }

    }
}
