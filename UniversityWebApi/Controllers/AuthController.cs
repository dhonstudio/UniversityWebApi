using Application.Features;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApi.Controllers
{
    public class AuthController(TokenFeature tokenFeature) : Controller
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel.Username == "string" && loginModel.Password == "string")
            {
                var token = tokenFeature.GenerateToken(loginModel.Username);

                var finalToken = new
                {
                    Token = token
                };
                return Ok(finalToken);
            }
            return Unauthorized();
        }
    }
}
