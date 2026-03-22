using BlazorApp.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Security
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
        }

        public string GenerateToken(Employee employee)
        {
            var claims = new[]
            {
                new Claim("dni", employee.Dni),
                new Claim("names", employee.Names),
                new Claim("father_lastname", employee.FatherLastname),
                new Claim("mother_lastname",employee.MotherLastname),
                new Claim("genre", employee.Genre),
                new Claim("email", employee.Email),
                new Claim("second_email", employee.SecondEmail ?? ""),
                new Claim("phone_number", employee.PhoneNumber),
                new Claim("second_phone_number", employee.SecondPhoneNumber ?? ""),
                new Claim("nationality", employee.Nationality ?? ""),
                new Claim("type_hire", employee.TypeHire),
            };

            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var hours = int.Parse(_config["Jwt:ExpirationHours"]!);

            var token = new JwtSecurityToken(
                issuer:             _config["Jwt:Issuer"],
                audience:           _config["Jwt:Audience"],
                claims:             claims,
                expires:            DateTime.UtcNow.AddHours(hours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var key     = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            try
            {
                return handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer           = true,
                    ValidateAudience         = true,
                    ValidateLifetime         = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer              = _config["Jwt:Issuer"],
                    ValidAudience            = _config["Jwt:Audience"],
                    IssuerSigningKey         = new SymmetricSecurityKey(key)
                }, out _);
            }
            catch
            {
                return null; // Invalid token
            }
        }
    }
}