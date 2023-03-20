using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.RequestFeatures
{
    public class UsersParameters : RequestParameters
    {
        public uint MinPoint { get; set; }
        public uint MaxPoint { get; set; } = int.MaxValue;
        public bool ValidatePointRange => MaxPoint > MinPoint;
        public string? SearchTerm { get; set; }
        public string OrderBy { get; set; }
    }
}
