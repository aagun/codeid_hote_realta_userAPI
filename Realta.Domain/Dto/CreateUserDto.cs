using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class CreateUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserPhoneNumber { get; set; }
        public int UspaUserId { get; set; }
        public Guid UspaPasswordHash { get; set; }
        public Guid UspaPasswordSalt { get; set; }
    }
}
