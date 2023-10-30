using FirstTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.ViewModels
{
    public class DashboardVM
    {
        public IEnumerable<Elan> Elans { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
