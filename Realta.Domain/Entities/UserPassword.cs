using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class UserPassword
    {
        [Key]
        public int uspa_user_id { get; set; }
        [ForeignKey("Users")]
        public string uspa_passwordHash { get; set; }
        public string uspa_passwordSalt { get; set; }
    }
}
