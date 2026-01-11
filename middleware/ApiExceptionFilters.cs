using System.Net;
using APIResponseWrapper;

public class ApiExceptionFilters
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionFilters> _logger;

    public ApiExceptionFilters(
        RequestDelegate next,
        ILogger<ApiExceptionFilters> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BadHttpRequestException ex)
        {
            await WriteError(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            await WriteError(context, HttpStatusCode.Unauthorized, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteError(context, HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    private static Task WriteError(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsJsonAsync(new ApiResponse<string>(false,message , statusCode:statusCode));
    }
}
