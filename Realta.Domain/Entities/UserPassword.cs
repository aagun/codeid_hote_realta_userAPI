using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class UserPassword
    {
        [Key]
        public int UspaUserId { get; set; }
        [ForeignKey("Users")]
        public string UspaPasswordHash { get; set; }
        public string UspaPasswordSalt { get; set; }
    }
}
