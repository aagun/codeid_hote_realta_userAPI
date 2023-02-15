using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UserMembersDto
    {
        public int usme_user_id { get; set; }
        public string usme_memb_name { get; set; }
        public DateTime? usme_promote_date { get; set; }
        public string? usme_points { get; set; }
        public string? usme_type { get; set; }
    }
}
