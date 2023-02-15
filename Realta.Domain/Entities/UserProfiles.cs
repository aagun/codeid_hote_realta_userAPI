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
        public int uspro_id { get; set; }
        public int uspro_user_id { get; set; }
        [ForeignKey("Users")]
        public int uspro_addr_id { get; set; }
        [ForeignKey("Address")]
        public string uspro_national_id { get; set; }
        public DateTime uspro_birth_date { get; set; }
        public string? uspro_job_title { get; set; }
        public string uspro_marital_status { get; set; }
        public string uspro_gender { get; set; }
        
        
    }
}
