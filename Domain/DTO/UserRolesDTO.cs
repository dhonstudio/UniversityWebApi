using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UserRolesDTO
    {
        public string Username { get; set; }
        public RoleNameEnum IDRole { get; set; }
        public string RoleName { get
            {
                return IDRole switch
                {
                    RoleNameEnum.Pengguna => "Pengguna",
                    RoleNameEnum.Administrator => "Administrator",
                    RoleNameEnum.Pengelola => "Pengelola",
                };
            }
        }
    }

    public enum RoleNameEnum
    {
        Pengguna,
        Administrator,
        Pengelola,
    }
}
