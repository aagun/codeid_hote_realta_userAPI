using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UsersDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserFullName { get; set; }
        public string? UserType { get; set; }
        public string? UserCompanyName { get; set; }
        public string? UserEmail { get; set; }
        [Required]
        public string UserPhoneNumber { get; set; }
        public DateTime? UserModifiedDate { get; set; }
    }
}
