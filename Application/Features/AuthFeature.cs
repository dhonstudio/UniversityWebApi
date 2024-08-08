using Application.Repositories;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features
{
    public class AuthFeature(
        IRepository<Users> repository, 
        IRepository<UserRoles> roleRepository, 
        IMapper mapper, 
        IConfiguration configuration,
        PasswordFeature passwordFeature,
        TokenFeature tokenFeature)
    {
        public async Task<object?> CreateToken(LoginModel loginModel)
        {
            var user = await repository.Get(x => x.Username == loginModel.Username);
            if (user.FirstOrDefault() == null)
            {
                throw new Exception("User tidak ditemukan");
            }

            var userParam = mapper.Map<UsersParamDTO>(user.FirstOrDefault());
            if (passwordFeature.VerifyPassword(user.FirstOrDefault().Password, loginModel.Password, userParam))
            {
                var token = tokenFeature.GenerateToken(loginModel.Username);

                var finalToken = new
                {
                    Token = token
                };
                return finalToken;
            }
            return null;
        }

        public async Task<object?> ChangeRole(string username, int roleid)
        {
            var user = await repository.Get(x => x.Username == username);

            var roleExist = await roleRepository.Get(x => x.IDUser == user.FirstOrDefault().ID);

            if (!roleExist.Select(x => x.IDRole).ToList().Contains(roleid))
            {
                throw new Exception("Tidak memiliki Role ini");
            }

            var userRoleDto = new UserRolesDTO()
            {
                IDRole = (RoleNameEnum)roleid,
            };
            var token = tokenFeature.GenerateToken(username, userRoleDto);

            var finalToken = new
            {
                Token = token
            };
            return finalToken;
        }

        public async Task<object?> ValidatePublic(HttpRequest request)
        {
            var clientId = request.Headers.FirstOrDefault(x => x.Key.ToLower() == "clientid").Value;
            var clientSecret = request.Headers.FirstOrDefault(x => x.Key.ToLower() == "clientsecret").Value;

            if (clientId == configuration["PublicToken:ClientId"] && clientSecret == configuration["PublicToken:ClientSecret"])
            {
                var token = tokenFeature.GeneratePublicToken();

                var finalToken = new
                {
                    Token = token
                };
                return finalToken;
            }

            throw new Exception("Tidak valid");
        }
    }
}
