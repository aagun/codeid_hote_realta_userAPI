using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("UserProfiles")]
    public class UserProfiles
    {
        [Key]
        public int UsproId { get; set; }
        public int UsproUserId { get; set; }
        [ForeignKey("Users")]
        public int UsproAddrId { get; set; }
        [ForeignKey("Address")]
        public string UsproNationalId { get; set; }
        public DateTime UsproBirthDate { get; set; }
        public string? UsproJobTitle { get; set; }
        public string UsproMaritalStatus { get; set; }
        public string UsproGender { get; set; }
        
        
    }
}
