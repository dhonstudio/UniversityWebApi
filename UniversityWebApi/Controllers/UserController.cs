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
        IRepository<UserRoles> roleRepository, 
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

        [HttpPost("addRole")]
        public async Task<IActionResult> AddRole(UserRoleParamDTO userRoleParam)
        {
            var userExist = await repository.Get(x => x.Username.Equals(userRoleParam.Username));

            if (userExist.FirstOrDefault() == null) return BadRequest(new
            {
                Message = "User tidak ada"
            });

            var roleExist = await roleRepository.Get(x => x.IDUser == userExist.FirstOrDefault().ID 
                && x.IDRole == userRoleParam.IDRole);

            if (roleExist.FirstOrDefault() != null) return BadRequest(new
            {
                Message = "Role sudah pernah ditambahkan"
            });

            var userRole = mapper.Map<UserRoles>(userRoleParam);
            userRole.IDUser = userExist.FirstOrDefault().ID;
            await roleRepository.Add(userRole);
            await unitOfWork.SaveChanges();

            return Ok(mapper.Map<UserRolesDTO>(userRole));
        }
    }
}
