using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TP4.Models.EntityFramework;

namespace TP4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private List<Utilisateur> appUsers = new List<Utilisateur>
        {
            new Utilisateur { Nom = "Vincent COUTURIER", Mail = "vince@gmail.com", Pwd = "12345678910@Ll", Role = "Admin" },
            new Utilisateur { Nom = "Marc MACHIN", Mail = "marc@gmail.com", Pwd = "12345678910@Ll", Role = "User" }
        };
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Utilisateur login)
        {
            IActionResult response = Unauthorized();
            Utilisateur user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
        private Utilisateur AuthenticateUser(Utilisateur user)
        {
            return appUsers.SingleOrDefault(x => x.Mail.ToUpper() == user.Mail.ToUpper() &&
           x.Pwd == user.Pwd);
        }
        private string GenerateJwtToken(Utilisateur
            userInfo)
        {
            var securityKey = new
           SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Mail),
                new Claim("fullName", userInfo.Nom.ToString()),
                new Claim("role",userInfo.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
