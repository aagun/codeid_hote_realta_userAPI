using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UserProfilesDto
    {
        public int uspro_id { get; set; }
        public string uspro_national_id { get; set; }
        public DateTime uspro_birth_date { get; set; }
        public string? uspro_job_title { get; set; }
        public string uspro_marital_status { get; set; }
        public string uspro_gender { get; set; }
        public int uspro_addr_id { get; set; }
        public int uspro_user_id { get; set; }
    }
}
