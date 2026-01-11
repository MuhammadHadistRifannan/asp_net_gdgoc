using System.Text;
using gdgoc_aspnet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);


builder.Services
.InjectDbContext(builder.Configuration)
.AddLibraryInjection()
.AddServiceInjection()
.AddJwtAuthentication(builder.Configuration)
.AddAuthorization()
.AddControllers();


var app = builder.Build();
app.UseAuth();
app.MapControllers();
app.Run();