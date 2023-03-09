using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UserProfilesDto
    {
        [Required]
        public int UsproId { get; set; }
        [Required]
        public string UsproNationalId { get; set; }
        [Required]
        public DateTime UsproBirthDate { get; set; }
        public string? UsproJobTitle { get; set; }
        [Required]
        public string UsproMaritalStatus { get; set; }
        [Required]
        public string UsproGender { get; set; }
        public int UsproAddrId { get; set; }
        public int UsproUserId { get; set; }
    }
}
