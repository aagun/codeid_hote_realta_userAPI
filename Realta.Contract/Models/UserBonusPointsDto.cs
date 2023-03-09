using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class UserBonusPointsDto
    {
        [Required]
        public int UbpoId { get; set; }
        [Required]
        public int UbpoUserId { get; set; }
        public Int16? UbpoTotalPoints { get; set; }
        public string? UbpoBonusType { get; set; }
        public DateTime? UbpoCreatedOn { get; set; }
    }
}
