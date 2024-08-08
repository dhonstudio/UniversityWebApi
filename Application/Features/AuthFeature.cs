using Application.Repositories;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features
{
    public class AuthFeature(
        IRepository<Users> repository, 
        IMapper mapper, 
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
    }
}
