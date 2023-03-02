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
        public int UsroUserId { get; set; }
        [ForeignKey("Users")]
        public int UsroRoleId { get; set; }
        
        
    }
}
