using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class RolesDto
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set;}
    }
}
