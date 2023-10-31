using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Models
{
    public class Brand :BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(1000)]
        [Required]
        public string BrandInfo { get; set; }
        [Required]
       
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
