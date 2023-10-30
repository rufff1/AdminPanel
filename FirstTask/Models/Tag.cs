﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Models
{
    public class Tag : BaseEntity
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public IEnumerable<Blog> Blogs { get; set; }


    }
}
