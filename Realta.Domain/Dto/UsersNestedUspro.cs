using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Dto
{
    public class UsersNestedUspro
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string? UserType { get; set; }
        public string? UserCompanyName { get; set; }
        public string? UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public virtual ICollection<UserProfiles>? UserProfiles { get; set; }
    }
}
