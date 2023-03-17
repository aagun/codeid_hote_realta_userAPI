using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Dto
{
    public class UsersJoinUsme
    {
        public string UserFullName { get; set; }
        public string? UserType { get; set; }
        public string? UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UsmeMembName { get; set; }
    }
}
