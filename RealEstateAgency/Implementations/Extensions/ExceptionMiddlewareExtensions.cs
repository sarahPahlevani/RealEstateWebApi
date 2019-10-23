using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using RealEstateAgency.Shared.BaseModels;
using RealEstateAgency.Shared.Exceptions;
using Serilog;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;

namespace RealEstateAgency.Implementations.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        //https://code-maze.com/global-error-handling-aspnetcore/
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger, IHostingEnvironment env)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        switch (contextFeature.Error)
                        {
                            case AppException _:
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                break;
                            case AppNotFoundException _:
                                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                break;
                            case ForbiddenException _:
                                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                break;
                            case AccessDeniedException _:
                                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                break;
                        }

                        logger.Error($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            StackTrace = env.IsDevelopment() ? contextFeature.Error.ToString(): ""
                        }.ToString());
                    }
                });
            });
        }
    }
}
