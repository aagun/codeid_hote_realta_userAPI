using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UserBonusPointsDto
    {
        public int ubpo_id { get; set; }
        public int ubpo_user_id { get; set; }
        public string ubpo_total_points { get; set; }
        public string ubpo_bonus_type { get; set; }
        public DateTime ubpo_created_on { get; set; }
    }
}
