using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.AuthenticationWebAPI
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? UserEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? UserPassword { get; set; }
        public string? ResponseMessage { get; set; }
        
        [Required]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string? UserPhoneNumber { get; set; }
    }
}
