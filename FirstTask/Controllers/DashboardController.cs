using FirstTask.DAL;
using FirstTask.Models;
using FirstTask.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Areas.Manage.Controllers
{
    public class DashboardController : Controller
    {


        private readonly UserManager<AppUser> _userManager;

        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public DashboardController(AppDbContext context, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _env = env;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index(DashboardVM dashboardVM)
        {
            DashboardVM dashboardVm = new DashboardVM
            {
                Elans = await _context.Elans.OrderBy(e=> e.Title)
                .Include(e => e.Category)
                .Where(e => e.IsDeleted == false).ToListAsync(),
                AppUsers = await _userManager.Users
                .OrderBy(e => e.Name).ToListAsync(),
                Blogs = await _context.Blogs.OrderBy(e => e.Title)
                .Include(b => b.category)
                .Where(b => b.IsDeleted == false).ToListAsync(),
                Brands = await _context.Brands.Where(b=> b.IsDeleted == false).ToListAsync()
                 
            };


            return View(dashboardVm);
        }


    }



}
