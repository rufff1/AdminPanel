using FirstTask.DAL;
using FirstTask.Models;
using FirstTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Controllers
{
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }



        public IActionResult Index(int pageIndex)
        {

            IQueryable<Tag> tags = _context.Tags.Where(b => b.IsDeleted == false);

            return View(PageNationList<Tag>.Create(tags, pageIndex, 3));
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {

            if (!ModelState.IsValid)
            {
                return View();

            }


            if (await _context.Tags.AnyAsync(t=> t.IsDeleted == false && t.Name.ToLower() == tag.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", $"This Name : {tag.Name} already exist");

                return View(tag);
            }

            tag.Name = tag.Name.Trim();
            tag.IsDeleted = false;
            tag.CreatAt = DateTime.UtcNow.AddHours(4);
            tag.CreatBy = "System";

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id )
        {
            if (id ==null)
            {
                return BadRequest("Id bos ola bilmez");

            }

            Tag tag = await _context.Tags.FirstOrDefaultAsync(t=> t.IsDeleted == false && t.Id == id);


            if (tag == null)
            {
                return NotFound("Daxil etdiyiniz Id yalnisdir");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Update(int? id, Tag tag)
        {

            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Tag existedTag = await _context.Tags.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (existedTag == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }


            if (tag.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }


            bool isExist = _context.Tags.Any(c => c.IsDeleted == false && c.Name.ToLower() == tag.Name.ToLower().Trim());
          
            if (isExist && !((existedTag.Name.ToLower() == tag.Name.ToLower().Trim())))
            {
                ModelState.AddModelError("", "Bu adda tag artig var");
                return View();
            };

            existedTag.Name = tag.Name.Trim();

            existedTag.UpdateAt = DateTime.UtcNow.AddHours(4);
            existedTag.UpdateBy = "System";
            existedTag.IsDeleted = false;

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");

         
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }


            Tag tag = await _context.Tags.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (tag == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(tag);

        }


        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Tag tag = await _context.Tags
                .Include(c => c.Blogs)
                
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (tag == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            if (tag.Id != id)
            {
                return BadRequest(" Id yalnisdir");
            }


          

            tag.IsDeleted = true;
            tag.DeletedAt = DateTime.UtcNow.AddHours(4);
            tag.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

    }
}
