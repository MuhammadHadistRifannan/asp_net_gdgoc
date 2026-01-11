using Microsoft.EntityFrameworkCore;

namespace gdgoc_aspnet;

public static class DbExtension
{

    public static IServiceCollection InjectDbContext(this IServiceCollection _services , IConfiguration _config)
    {
        _services.AddDbContext<AppDbContext>(
            opt => opt.UseNpgsql(_config.GetSection("postgres").GetValue<string>("connection-string"))
        );   
        return _services;
    }
}
