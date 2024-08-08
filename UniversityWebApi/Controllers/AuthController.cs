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
        AuthFeature authFeature,
        IMapper mapper) : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var token = await authFeature.CreateToken(loginModel);
                if (token != null)
                {
                    return Ok(token);
                }

                return Unauthorized();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
