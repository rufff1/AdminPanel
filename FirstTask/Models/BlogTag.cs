using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Models
{
    public class BlogTag : BaseEntity
    {
        public Blog Blog { get; set; }
        public int BlogId { get; set; }

        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
