using FirstTask.DAL;
using FirstTask.Models;
using FirstTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Controllers
{
    [Authorize(Roles = "HR,Manager")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int pageIndex)
        {

            IQueryable<Category> categories = _context.Categories.Where(b => b.IsDeleted == false);

            return View(PageNationList<Category>.Create(categories, pageIndex, 3));
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (await _context.Categories.AnyAsync(c => c.IsDeleted == false && c.Name.ToLower() == category.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", $"This Name : {category.Name} already exist");

                return View(category);
            }

            category.Name = category.Name.Trim();

            category.IsDeleted = false;
            category.CreatBy = "System";
            category.CreatAt = DateTime.UtcNow.AddHours(4);

            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (category == null)
            {
                return NotFound("Daxil etdiyiniz Id yalnisdir");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Category category, int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Category existedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (existedCategory == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }


            if (category.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }


            bool isExist = _context.Categories.Any(c => c.IsDeleted == false && c.Name.ToLower() == category.Name.ToLower().Trim());
            if (isExist && !((existedCategory.Name.ToLower() == category.Name.ToLower().Trim())))
            {
                ModelState.AddModelError("", "Bu adda category artig var");
                return View();
            };

            existedCategory.Name = category.Name.Trim();

            existedCategory.UpdateAt = DateTime.UtcNow.AddHours(4);
            existedCategory.UpdateBy = "System";
            existedCategory.IsDeleted = false;

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }


            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (category == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(category);

        }


        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Category category = await _context.Categories
                .Include(c => c.Elans)
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (category == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            if (category.Id != id)
            {
                return BadRequest(" Id yalnisdir");
            }


            if (category.Elans.Count() > 0)
            {
                return Json(new { status = 400 });
            }

            category.IsDeleted = true;
            category.DeletedAt = DateTime.UtcNow.AddHours(4);
            category.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index", new { status = 200 });
        }

    }
}
