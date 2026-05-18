using Microsoft.AspNetCore.Diagnostics;

namespace WorkBoard.API.Extensions;

public static class MiddlewareExtensions
{
    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseCors();
        app.UseAuthorization();

        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var exception = context.Features
                    .Get<IExceptionHandlerFeature>()?
                    .Error;

                var response = exception switch
                {
                    InvalidOperationException ex => new
                    {
                        code = ex.Message, 
                        message = ex.Message
                    },
                    _ => new
                    {
                        code = "UNKNOWN_ERROR",
                        message = "Something went wrong"
                    }
                };

                context.Response.StatusCode = 400;

                await context.Response.WriteAsJsonAsync(response);
            });
        });
    }
}
