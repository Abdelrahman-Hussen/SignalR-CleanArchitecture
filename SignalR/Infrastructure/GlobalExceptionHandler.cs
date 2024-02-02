using LurkingUnits.Application;
using Microsoft.AspNetCore.Diagnostics;
using SignalR.Application;
using SignalR.Application.Features.System.Resources;
using System.Net;

namespace SignalR.Infrastructure
{
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
                                              Exception exception,
                                              CancellationToken cancellationToken)
        {
            var (statusCode, errorMessage) = GetExaptionType(exception);

            _logger.Log(LogLevel.Error, exception.ToString());

            httpContext.Response.StatusCode = statusCode;

            var data = httpContext.Request.Method == HttpMethod.Get.ToString() ? httpContext.Request.QueryString.ToString() : httpContext.Request.Body.ToString();

            await httpContext.Response
                .WriteAsJsonAsync(
                    ResponseModel<string>
                    .Error(errorMessage, data),
                    cancellationToken);

            return true;
        }

        private (int statusCode, string errorMessage) GetExaptionType(Exception exception)
            => exception switch
            {
                NotFoundException => ((int)HttpStatusCode.NotFound, exception.Message),
                BadRequestException => ((int)HttpStatusCode.BadRequest, exception.Message),
                _ => ((int)HttpStatusCode.InternalServerError,
                Message.Error_General)
            };
    }
}
