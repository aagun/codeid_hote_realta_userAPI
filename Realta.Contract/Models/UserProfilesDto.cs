using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UserProfilesDto
    {
        public int UsproId { get; set; }
        public string UsproNationalId { get; set; }
        public DateTime UsproBirthDate { get; set; }
        public string? UsproJobTitle { get; set; }
        public string UsproMaritalStatus { get; set; }
        public string UsproGender { get; set; }
        public int UsproAddrId { get; set; }
        public int UsproUserId { get; set; }
    }
}
