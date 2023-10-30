using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FirstTask.Models
{
    public class Elan :BaseEntity
    {
        [StringLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        [StringLength(150)]
        public string Image { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public List<ElanTag> ElanTags { get; set; }


        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public IEnumerable<int> TagIds { get; set; }

    }
}
