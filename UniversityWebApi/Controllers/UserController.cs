using Application.Features;
using Application.Repositories;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController(
        IMapper mapper, 
        IRepository<Users> repository, 
        IUnitOfWork unitOfWork,
        PasswordFeature passwordFeature) : Controller
    {
        [HttpPost()]
        public async Task<IActionResult> Create(UsersParamDTO userParam)
        {
            var user = mapper.Map<Users>(userParam);
            var userExist = await repository.Get(x => x.Username == userParam.Username);
            if (userExist.FirstOrDefault() != null) return BadRequest(new
            {
                Message = "username sudah pernah didaftarkan"
            });
            user.Password = passwordFeature.HashPassword(userParam.Password, userParam);

            await repository.Add(user);
            await unitOfWork.SaveChanges();
            return Ok(mapper.Map<UsersDTO>(user));
        }
    }
}
