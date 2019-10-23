using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.Providers;

namespace RealEstateAgency.Implementations.Middleware
{
    public class UserProviderMiddleware
    {
        private readonly RequestDelegate _next;
        public UserProviderMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext, IUserProvider userProvider, ILanguageProvider languageProvider)
        {
            userProvider.SetUser(httpContext.User, languageProvider.SelectedLanguage.Id);
            await _next(httpContext);
        }
    }
}
