using Application.Features;
using Application.Repositories;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApi.Controllers
{
    public class AuthController(TokenFeature tokenFeature, IRepository<Users> repository) : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await repository.Get(x => x.Username == loginModel.Username && x.Password == loginModel.Password);
            if (user.FirstOrDefault() != null)
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
