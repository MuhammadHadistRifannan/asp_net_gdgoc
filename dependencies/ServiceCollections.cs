using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace gdgoc_aspnet;

public static class ServiceCollections
{
    public static IServiceCollection AddLibraryInjection(this IServiceCollection _services)
    {
        _services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
        _services.AddAutoMapper(typeof(Program));
        return _services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection _services, IConfiguration _config)
    {
        var jwt = _config.GetSection("Jwt");

        _services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(
            option =>
            {
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["SecretKey"]!))
                };

                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        ctx.NoResult();
                        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        ctx.Response.ContentType = "application/json";

                        return ctx.Response.WriteAsync("""
                                {
                                    "error": "invalid_token",
                                    "message": "Token is invalid or expired"
                                }
                        """);
                    },

                    OnChallenge = ctx =>
                    {
                        ctx.HandleResponse(); // 🔥 WAJIB

                        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        ctx.Response.ContentType = "application/json";

                        return ctx.Response.WriteAsync("""
                            {
                                "error": "unauthorized",
                                "message": "Authentication required"
                            }
                            """);
                    },

                    OnForbidden = ctx =>
                    {
                        ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                        ctx.Response.ContentType = "application/json";

                        return ctx.Response.WriteAsync("""
                            {
                                "error": "forbidden",
                                "message": "You do not have permission to access this resource"
                            }
                            """);
                                    }
                };
            }
        );

        return _services;
    }

    public static IServiceCollection AddServiceInjection(this IServiceCollection _services)
    {
        _services.AddSingleton<IJwtHandler, JwtService>();
        _services.AddScoped<ILoginService, LoginService>();
        _services.AddScoped<IRegisterService, RegisterService>();
        _services.AddScoped<IUpdateService, UpdateService>();
        _services.AddScoped<IDeleteService,DeleteService>();
        _services.AddScoped<IUserRepository, UserRepository>();

        return _services;
    }
}
