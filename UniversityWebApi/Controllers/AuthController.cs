using Application.Features;
using Application.Repositories;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace UniversityWebApi.Controllers
{
    public class AuthController(
        TokenFeature tokenFeature, 
        IRepository<Users> repository, 
        IRepository<UserRoles> roleRepository, 
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

        [HttpPost("publicLogin")]
        public async Task<IActionResult> PublicLogin([FromHeader] string ClientId, [FromHeader] string ClientSecret)
        {
            try
            {
                var token = await authFeature.ValidatePublic(Request);
                if (token != null)
                {
                    return Ok(token);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("changeRole")]
        public async Task<IActionResult> ChangeRole([FromBody] UserRoleParamDTO roleParam)
        {
            try
            {
                var claim = User.Claims;
                var username = claim.FirstOrDefault(x => x.Type == "username").Value;

                var roleid = roleParam.IDRole;

                var token = await authFeature.ChangeRole(username, roleid);
                if (token != null)
                {
                    return Ok(token);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
