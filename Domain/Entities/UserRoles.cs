using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserRoles : BaseEntity
    {
        public int ID { get; set; }
        public int IDUser { get; set; }
        public int IDRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [NotMapped]
        public string UserName { get; set; }
    }
}
