using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class ChangePasswordDto
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare(nameof(NewPassword), ErrorMessage = "New Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

        public string ResponseMessage { get; set; }
    }
}
