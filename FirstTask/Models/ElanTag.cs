using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Models
{
    public class ElanTag :BaseEntity
    {
        public Elan Elan { get; set; }
        public int ElanId { get; set; }

        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
