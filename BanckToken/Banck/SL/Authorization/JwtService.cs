using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Entities;



namespace SL.Authorization
{
    public static class JwtService
    {
        // Clave secreta (utiliza una cadena larga y segura, idealmente en configuración)
        private static readonly string SecretKey = "f5aN7jVh9F2LwB3zD6rM8qS1pW0tX4kY";
        private static readonly string Issuer = "tu_issuer";
        private static readonly string Audience = "tu_audience";

        public static string GenerateToken(string email, string role, int userId, int expireMinutes = 60)
        {
            var key = Encoding.ASCII.GetBytes(SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            // Definir las claims que incluirá el token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId", userId.ToString())
            };

            // Crear el descriptor del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
