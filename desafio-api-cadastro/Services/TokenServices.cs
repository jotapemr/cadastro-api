using desafio_api_cadastro.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace desafio_api_cadastro.Services
{
    public class TokenServices
    {
        public static string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyEncryption = Encoding.ASCII.GetBytes(KeyJWT.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                     new Claim(ClaimTypes.Name, user.name),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyEncryption), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
