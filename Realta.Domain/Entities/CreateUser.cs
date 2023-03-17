using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class CreateUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserPhoneNumber { get; set; }
        public string ResponseMessage { get; set; }
        public int UspaUserId { get; set; }
        public Guid UspaPasswordHash { get; set; }
        public Guid UspaPasswordSalt { get; set; }

    }
}
