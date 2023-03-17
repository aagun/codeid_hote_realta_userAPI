using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Dto
{
    public class UsersNestedUsme
    {
        public string UserFullName { get; set; }
        public string? UserType { get; set; }
        public string? UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public virtual ICollection<UserMembers> UserMembers { get; set; }
    }
}
