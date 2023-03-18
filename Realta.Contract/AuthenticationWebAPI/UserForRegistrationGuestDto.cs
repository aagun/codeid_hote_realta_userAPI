using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.AuthenticationWebAPI
{
    public class UserForRegistrationGuestDto
    {
        [Required(ErrorMessage = "Phone Number is required")]
        public string? UserPhoneNumber { get; set; }
        public string? ResponseMessage { get; set; }
    }
}
