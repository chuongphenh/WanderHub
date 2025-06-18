using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WanderHub.Application.Abstractions;
using WanderHub.Infrastructure.DependencyInjection.Options;

namespace WanderHub.Infrastructure.Authentication;
public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOption jwtOption = new JwtOption();

    public JwtTokenService(IConfiguration configuration)
    {
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: jwtOption.Issuer,
            audience: jwtOption.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtOption.ExpireMin),
            signingCredentials: signinCredentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        var radomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(radomNumber);
            return Convert.ToBase64String(radomNumber);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var key = Encoding.UTF8.GetBytes(jwtOption.SecretKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
        }
    }
}
