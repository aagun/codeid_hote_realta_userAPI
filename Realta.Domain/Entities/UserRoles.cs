using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class UserRoles
    {
        [Key]
        public int usro_user_id { get; set; }
        [ForeignKey("Users")]
        public int usro_role_id { get; set; }
        
        
    }
}
