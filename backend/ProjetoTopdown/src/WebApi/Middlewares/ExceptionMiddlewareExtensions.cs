using Microsoft.AspNetCore.Diagnostics;
using ProjetoTopdown.Application.Exceptions;
using ProjetoTopdown.WebApi.Models;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace ProjetoTopdown.WebApi.Middlewares;

#pragma warning disable CA1848 // Usar os delegados LoggerMessage

public static class ExceptionMiddlewareExtensions
{
    public const string DefaultErrorMessage = "Something went wrong, please try again later.";

    private static readonly Dictionary<Type, HttpStatusCode> ExceptionStatusCodes =
        new()
        {
            { typeof(FluentValidation.ValidationException), HttpStatusCode.BadRequest },

            { typeof(ProjetoTopdown.Application.Exceptions.ValidationException),
                HttpStatusCode.BadRequest },

            { typeof(NotFoundException), HttpStatusCode.NotFound },
            { typeof(BadHttpRequestException), HttpStatusCode.BadRequest },
            { typeof(UnauthorizedAccessException), HttpStatusCode.Forbidden },
        };

    private static readonly
        Dictionary<HttpStatusCode, Action<ILogger, Exception>> StatusCodeLogActions =
        new()
        {
                {
                    HttpStatusCode.Forbidden,
                    (logger, exception) => logger.LogInformation(
                        exception,
                        "Forbidden: {ExceptionMessage}",
                        exception.Message)
                },
                {
                    HttpStatusCode.NotFound,
                    (logger, exception) => logger.LogWarning(
                        exception,
                        "Not Found: {ExceptionMessage}",
                        exception.Message)
                },
                {
                    HttpStatusCode.BadRequest,
                    (logger, exception) => logger.LogWarning(
                        exception,
                        "Bad Request: {ExceptionMessage}",
                        exception.Message)
                },
                {
                    HttpStatusCode.InternalServerError,
                    (logger, exception) => logger.LogError(
                        exception,
                        "Something went wrong: {ExceptionMessage}",
                        exception.Message)
                },
        };

    public static void UseExceptionHandler(
        this IApplicationBuilder app,
        IHostEnvironment environment,
        ILogger logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                var statusCode = GetStatusCode(exception);

                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                if (exception != null)
                {
                    WriteLog(logger, exception, statusCode);

                    await WriteResponseAsync(environment, context, exception).ConfigureAwait(false);
                }
            });
        });
    }

    private static Task WriteResponseAsync(
            IHostEnvironment environment,
            HttpContext context,
            Exception exception)
    {

        var message = 
            context.Response.StatusCode == (int)HttpStatusCode.InternalServerError 
            && !environment.IsDevelopment()
            ? DefaultErrorMessage
            : exception.Message;

        var errorResponse = ApiResponse.Error(message);

        var jsonResponse = JsonSerializer.Serialize(errorResponse);
        return context.Response.WriteAsync(jsonResponse);
    }

    private static void WriteLog(
        ILogger logger,
        Exception exception,
        HttpStatusCode statusCode)
    {
        if (StatusCodeLogActions.TryGetValue(statusCode, out var exceptionAction))
        {
            exceptionAction.Invoke(logger, exception);
        }
    }

    private static HttpStatusCode GetStatusCode(Exception? exception)
    {
        if (exception == null)
        {
            return HttpStatusCode.InternalServerError;
        }

        return ExceptionStatusCodes.TryGetValue(exception.GetType(), out var exceptionStatusCode)
            ? exceptionStatusCode
            : HttpStatusCode.InternalServerError;
    }
}
