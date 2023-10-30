using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Models
{
    public class Category :BaseEntity
    {

        [StringLength(150)]
        public string Name { get; set; }

        public IEnumerable<Elan> Elans { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
