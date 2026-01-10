using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace gdgoc_aspnet;

public class JwtService : IJwtHandler
{

    readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    T GetMetadataJwt<T>(string jwtSection)
    {
        var section = _configuration.GetSection("Jwt");
        return section.GetValue<T>(jwtSection)!;
    }

    public string Generate(User user)
    {
        var issuer = GetMetadataJwt<string>("Issuer");
        var secret = GetMetadataJwt<string>("SecretKey");
        var aud = GetMetadataJwt<string>("Audience");

        var claims = new List<Claim>
        {
            new Claim("id" , user.id.ToString()),
        };

        var token = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var signingKey = new SigningCredentials(token , SecurityAlgorithms.HmacSha256);
        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: issuer , 
            audience: aud, 
            expires:DateTime.UtcNow.AddDays(7),
            claims: claims,
            signingCredentials:signingKey
        );
        
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }    
}
