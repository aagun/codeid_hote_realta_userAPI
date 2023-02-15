using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UserPasswordDto
    {
        public int uspa_user_id { get; set; }
        public string uspa_passwordHash { get; set; }
        public string uspa_passwordSalt { get; set; }
    }
}
