using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class UserBonusPoints
    {
        [Key]
        public int ubpo_id { get; set; }
        public int ubpo_user_id { get; set; }
        [ForeignKey("Users")]
        public Int16? ubpo_total_points { get; set; }
        public string? ubpo_bonus_type { get; set; }
        public DateTime? ubpo_created_on { get; set; }
    }
}
