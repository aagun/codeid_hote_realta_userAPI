using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UsersDto
    {
        public int user_id { get; set; }
        public string user_full_name { get; set; }
        public string? user_type { get; set; }
        public string? user_company_name { get; set; }
        public string? user_email { get; set; }
        public string user_phone_number { get; set; }
        public DateTime? user_modified_date { get; set; }
    }
}
