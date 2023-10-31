using FirstTask.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.ViewModels
{
    public class ProfileVM
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public AppUser AppUser { get; set; }

        [Required]
        public string Job { get; set; }

        public State State { get; set; }

        public int? StateId { get; set; }

        [Required]
        public string Adress { get; set; }
        [Required]

        public int Salary { get; set; }
        public string Phone { get; set; }

        [StringLength(1000)]
        public string UserImage { get; set; }
        [DataType(DataType.Password)]
        public string CurrentPaswoord { get; set; }

        [DataType(DataType.Password)]
        public string NewPaswoord { get; set; }

        [Compare(nameof(NewPaswoord))]
        [DataType(DataType.Password)]
        public string ConfirmPaswoord { get; set; }
        public IFormFile UserImageFile { get; set; }
    }
}
