using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UserMembersDto
    {
        public int UsmeUserId { get; set; }
        public string UsmeMembName { get; set; }
        public DateTime? UsmePromoteDate { get; set; }
        public Int16? UsmePoints { get; set; }
        public string? UsmeType { get; set; }
    }
}
