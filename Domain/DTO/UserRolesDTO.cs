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
        public int IDRole { get; set; }
        public string RoleName { get
            {
                return IDRole switch
                {
                    0 => "Pengguna",
                    1 => "Administrator",
                    2 => "Pengelola",
                };
            }
        }
    }
}
