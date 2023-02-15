using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class UserMembers
    {
        [Key]
        public int usme_user_id { get; set; }
        public string usme_memb_name { get; set; }
        [ForeignKey("Members")]
        public DateTime? usme_promote_date { get; set; }
        public Int16? usme_points { get; set; }
        public string? usme_type { get; set; }
    }
}
