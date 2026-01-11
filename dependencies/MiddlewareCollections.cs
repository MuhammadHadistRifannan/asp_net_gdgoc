namespace gdgoc_aspnet;

public static class MiddlewareCollections
{
    public static WebApplication UseAuth(this WebApplication _builder)
    {
        _builder.UseAuthentication();
        _builder.UseMiddleware<ApiExceptionFilters>();
        _builder.UseAuthorization();
        return _builder;
    }


}
