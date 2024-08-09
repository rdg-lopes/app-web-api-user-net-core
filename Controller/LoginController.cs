using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace UserWebApi.Controllers;

[ApiController]
[Route("/login")]
public class LoginController : ControllerBase
{
    private IConfiguration configuration;

    public LoginController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpPost]
    public IActionResult Logar(LoginModelViewInput loginInput)
    {        
        var secret = Encoding.ASCII.GetBytes(this.configuration.GetSection("JwtConfigurations:Secret").Value);
        var symmetricSecurityKey = new SymmetricSecurityKey(secret);
        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, loginInput.Id.ToString()),
                new Claim(ClaimTypes.Name, loginInput.Login)
           }),
           Expires = DateTime.UtcNow.AddDays(1),
           SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);
        Thread.Sleep(2000);
        return Ok(new {
                    Login = loginInput,
                    Token = token
                });
    }
}