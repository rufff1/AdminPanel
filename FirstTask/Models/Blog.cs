using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Models
{
    public class Blog :BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }


        public Category category { get; set; }
        public int CategoryId { get; set; }

       public List<BlogTag> BlogTags { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        [MaxLength(3)]
        public IEnumerable<int> TagIds { get; set; }
    }
}
