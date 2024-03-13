using Microsoft.AspNetCore.Diagnostics;
using SoclukProject.Common.Infrastructure.Exceptions;
using SoclukProject.Common.Infrastructure.Results;
using System.Net;

namespace SoclukProject.Api.WebApi.Infrastructure.Extensions;
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app,
        bool includeExceptionDetails = false,
        bool useDefaultHandlingResponse = true,
        Func<HttpContext, Exception, Task> handleException = null)
    {
        app.UseExceptionHandler(opt =>
        {
            opt.Run(context =>
            {
                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                if (!useDefaultHandlingResponse && handleException == null)
                    throw new ArgumentNullException(nameof(handleException),
                        $"{nameof(handleException)} cannot be null when {nameof(useDefaultHandlingResponse)} is false.");

                if (!useDefaultHandlingResponse && handleException != null)
                    return handleException(context, exceptionObject.Error);

                return DefaultException(context, exceptionObject.Error, includeExceptionDetails);
            });
        });


        return app;
    }

    private static async Task DefaultException(HttpContext context, Exception exception, bool includeExceptionDetails)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "Internal server error occured";

        if (exception is UnauthorizedAccessException)
            statusCode = HttpStatusCode.Unauthorized;

        if (exception is DatabaseValidationException)
        {
            var validationResponse = new ValidationResponseModel(exception.Message);
            await WriteResponse(context, statusCode, validationResponse);
            return;
        }
        var response = new
        {
            HttpStatusCode = (int)statusCode,
            Detail = includeExceptionDetails ? exception.ToString() : message,
        };

        await WriteResponse(context, statusCode, response);
    }

    private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, object responseObject)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsJsonAsync(responseObject);
    }
}

