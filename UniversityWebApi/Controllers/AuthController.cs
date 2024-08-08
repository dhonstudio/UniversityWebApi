using Application.Features;
using Application.Repositories;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApi.Controllers
{
    public class AuthController(
        TokenFeature tokenFeature, 
        IRepository<Users> repository, 
        PasswordFeature passwordFeature,
        IMapper mapper) : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await repository.Get(x => x.Username == loginModel.Username);
            if (user.FirstOrDefault() == null)
            {
                return NotFound();
            }

            var userParam = mapper.Map<UsersParamDTO>(user.FirstOrDefault());
            if (passwordFeature.VerifyPassword(user.FirstOrDefault().Password, loginModel.Password, userParam))
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
