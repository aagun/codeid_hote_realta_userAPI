﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class CreateProfile
    {
        public string UserFullName { get; set; }
        public string? UserType { get; set; }
        public string? UserCompanyName { get; set; }
        public string? UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UsproNationalId { get; set; }
        public DateTime UsproBirthDate { get; set; }
        public string? UsproJobTitle { get; set; }
        public string UsproMaritalStatus { get; set; }
        public string UsproGender { get; set; }
        public int UsproAddrId { get; set; }
    }
}
