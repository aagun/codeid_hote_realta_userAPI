using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UserPasswordDto
    {
        public int UspaUserId { get; set; }
        public string UspaPasswordHash { get; set; }
        public string UspaPasswordSalt { get; set; }
    }
}
