using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mappy.Configurations.Models;
using Mappy.Models.Responses;
using Microsoft.IdentityModel.Tokens;

namespace Mappy.Helpers;

public static class AuthenticationHelper
{
  public static object GenerateToken(SecureUserModel user, JwtConfigurationModel jwtSettings)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new Claim[]
      {
        new Claim(ClaimTypes.UserData, user.Id.ToString())
      }),

      Expires = DateTime.UtcNow.AddSeconds(jwtSettings.ExpiryTimeInSeconds),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
      Audience = jwtSettings.Audience,
      Issuer = jwtSettings.Issuer
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}