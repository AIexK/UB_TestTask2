using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;
using TestTask.Application.Common.Interfaces;

namespace TestTask.Infrastructure.Middlewares;

public class ApiExceptionFilter : IExceptionFilter
{
    private ICustomLogger _logger;
    public ApiExceptionFilter(ICustomLogger logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var ex = context.Exception;
        var route = $"{context?.ActionDescriptor?.DisplayName}";
        var exceptionType = context.Exception.GetType();

        context.ExceptionHandled = true;

        switch (context.Exception)
        {
            case TaskCanceledException:
                _logger?.Debug($"{route}: Action was canceled");
                return;

            case KeyNotFoundException:
                context.Result = new StatusCodeResult((int)HttpStatusCode.NotFound);
                return;
            case JsonException:
            case FormatException:
            case InvalidOperationException:
            case ArgumentOutOfRangeException:
            case ArgumentNullException:
            case ArgumentException:
            case InvalidDataException:
            case InvalidCastException:
                _logger?.Debug($"{route} - {context.Exception.GetType().Name}: {ex.Message}{Environment.NewLine}{ex.InnerException}");
                context.Result = new BadRequestObjectResult(ex.Message?.ToString() ?? string.Empty);
                return;

            default:
                _logger?.Error($"{route} - {context.Exception.GetType().Name}: {ex.Message}{Environment.NewLine}{ex.InnerException?.Message}{Environment.NewLine}{ex.StackTrace}");
                context.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);
                return;
        }
    }
}
