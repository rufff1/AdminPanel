using FirstTask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
         public DbSet<Elan> Elans { get; set; }
         public DbSet<Category> Categories { get; set; }
         public DbSet<Blog> Blogs { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ElanTag> ElanTags { get; set; }
        public DbSet<Brand> Brands { get; set; }



    }
}
