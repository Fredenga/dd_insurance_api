using InsuranceAPI.Entities;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace InsuranceAPI.Auth
{
    public class Token(IConfiguration config)
    {
        public string Create(Admin admin)
        {
            string secretKey = config["JWT:Secret"!]!;
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, admin.AdminId.ToString()),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, admin.Email),

                    ]
                ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials,
                Issuer = config["JWT:Issuer"!]!,
                Audience = config["JWT:Audience"!]!
            };

            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
