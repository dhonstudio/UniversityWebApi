using Domain.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features
{
    public class PasswordFeature(IPasswordHasher<object> passwordHasher)
    {
        public string HashPassword(string password, UsersParamDTO userParam)
        {
            return passwordHasher.HashPassword(userParam, password);
        }

        public bool VerifyPassword(string hashedPassword, string password, UsersParamDTO userParam)
        {
            var result = passwordHasher.VerifyHashedPassword(userParam, hashedPassword, password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
