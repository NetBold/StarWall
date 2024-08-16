using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using StarWall.Core.Models;

namespace StarWall.API.Services;

public static class ServiceExtention
{
    public static void ConfigureExeptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(e => e.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "Application/json";
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                Log.Error($"Something went wrong in the {contextFeature.Error}");
                await context.Response.WriteAsync(new Error
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = contextFeature.Error.InnerException.ToString()
                }.ToString());
            }
        }));
    }
}