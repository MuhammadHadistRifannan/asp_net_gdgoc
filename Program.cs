using System.Text;
using gdgoc_aspnet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var sectionJwt = builder.Configuration.GetSection("Jwt");

var signingKey = sectionJwt.GetValue<string>("SecretKey");
var issuer = sectionJwt.GetValue<string>("Issuer");
var issued = sectionJwt.GetValue<string>("Issued_At");
var audience = sectionJwt.GetValue<string>("Audience");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetSection("postgres").GetValue<string>("connection-string"));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly , includeInternalTypes: true);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<IJwtHandler , JwtService>();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(
    option =>
    {

        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!))
        };

        
    }
);

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();