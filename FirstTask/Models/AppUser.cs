using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FirstTask.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(20)]
        public string Name { get; set; }

       
        [Required]
        public int Salary { get; set; }

        public string Job { get; set; }
   
        [StringLength(150)]
        public string Adress { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public int? StateId { get; set; }

        public State State { get; set; }
       

        [StringLength(1000)]
        public string UserImage { get; set; }
        [NotMapped]
        public IFormFile UserImageFile { get; set; }
        public string EmailConfirmationToken { get; set; }
        public string PasswordResetToken { get; set; }
        public bool isConfirmed { get; set; }
        public string ConnectionId { get; set; }

  
    }
}
