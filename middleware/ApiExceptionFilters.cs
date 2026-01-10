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
            await WriteError(context, 400, ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            await WriteError(context, 401, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteError(context, 500, "Internal server error");
        }
    }

    private static Task WriteError(
        HttpContext context,
        int statusCode,
        string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsJsonAsync(new
        {
            success = false,
            message
        });
    }
}
