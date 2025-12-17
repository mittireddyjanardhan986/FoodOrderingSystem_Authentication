using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodOrderingApplicationFigma.Services.Service_Folder
{
    public class OtpService : IOtpService
    {
        private readonly IConfiguration _configuration;

        public OtpService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateOtp()
        {
            return new Random().Next(100000, 999999).ToString();
        }

        public string GenerateOtpToken(string otp, string identifier)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("otp", otp),
                    new Claim("identifier", identifier)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateOtpToken(string token, string otp, string identifier)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
                var tokenHandler = new JwtSecurityTokenHandler();

                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                var storedOtp = claimsPrincipal.FindFirst("otp")?.Value;
                var storedIdentifier = claimsPrincipal.FindFirst("identifier")?.Value;

                return storedOtp == otp && storedIdentifier == identifier;
            }
            catch
            {
                return false;
            }
        }
    }
}
