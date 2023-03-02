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
        public int UbpoId { get; set; }
        public int UbpoUserId { get; set; }
        [ForeignKey("Users")]
        public Int16? UbpoTotalPoints { get; set; }
        public string? UbpoBonusType { get; set; }
        public DateTime? UbpoCreatedOn { get; set; }
    }
}
