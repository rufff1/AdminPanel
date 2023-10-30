using FirstTask.DAL;
using FirstTask.Extensions;
using FirstTask.Helpers;
using FirstTask.Models;
using FirstTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Controllers
{

    [Authorize(Roles = "HR,Manager")]
    public class ElanController : Controller
    {

        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;

        public ElanController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int pageIndex)
        {



            IQueryable<Elan> categories = _context.Elans
                .Include(e => e.Category)
                .Include(e=> e.ElanTags)
                 .ThenInclude(et=> et.Tag)
                .Where(b => b.IsDeleted == false);

            return View(PageNationList<Elan>.Create(categories, pageIndex, 3));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Category = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tag = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Elan elan)
        {
            ViewBag.Category = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tag = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!await _context.Categories.AnyAsync(c => c.IsDeleted == false && c.Id == elan.CategoryId))
            {
                ModelState.AddModelError("ElanCategoryId", "gelen categoriya yalnisdir");
                return View(elan);
            }


            List<ElanTag> elanTags = new List<ElanTag>();

            foreach (int tagId in elan.TagIds)
            {
                if (elan.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    ModelState.AddModelError("TagIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(elan);

                }

                if (!await _context.Tags.AnyAsync(t => t.IsDeleted == false && t.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "secilen tag yalnisdir");
                    return View(elan);
                }

                ElanTag elanTag = new ElanTag
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    TagId = tagId

                };


                elanTags.Add(elanTag);
            }


            if (elan.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!elan.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!elan.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }
            elan.Image = elan.ImageFile.CreateImage(_env, "manage", "assets", "img", "Elan-photo");
            elan.Title = elan.Title;
            elan.Description = elan.Description;
            elan.ElanTags = elanTags;
            elan.CreatBy = "System";
            elan.IsDeleted = false;
            elan.CreatAt = DateTime.UtcNow.AddHours(4);
            await _context.Elans.AddAsync(elan);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Category = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tag = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();
           
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Elan elan = await _context.Elans.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);




            if (elan == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            elan.TagIds = await _context.ElanTags.Where(pt => pt.ElanId == id).Select(x => x.TagId).ToListAsync();


            return View(elan);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Update(int? id, Elan elan)
        {
            ViewBag.Category = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tag = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();


            if (!ModelState.IsValid) return View();

            if (id == null) return BadRequest("Id daxil edin");

            if (elan.Id != id)
            {
                return BadRequest("Id yalnisdir");
            }

            Elan existedelan = await _context.Elans
                .Include(e=> e.ElanTags)
                .FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id);


            _context.ElanTags.RemoveRange(existedelan.ElanTags);

            List<ElanTag> elanTags = new List<ElanTag>();

            foreach (int tagId in elan.TagIds)
            {
                if (elan.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    ModelState.AddModelError("TagIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(elan);

                }

                if (!await _context.Tags.AnyAsync(t => t.IsDeleted == false && t.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "secilen tag yalnisdir");
                    return View(elan);
                }

                ElanTag elanTag = new ElanTag
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    TagId = tagId

                };


                elanTags.Add(elanTag);
            }

            if (existedelan == null)
            {
                return NotFound("Bele bir melumat yoxdur. Id yalnisdir");
            }


            if (elan.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!elan.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!elan.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }
            Helper.DeleteFile(_env, existedelan.Image, "manage", "assets", "img", "Elan-photo");
            existedelan.Image = elan.ImageFile.CreateImage(_env, "manage", "assets", "img", "Elan-photo");
            existedelan.Title = elan.Title;
            existedelan.Description = elan.Description;
            existedelan.ElanTags = elanTags;
            elan.UpdateBy = "System";
            elan.IsDeleted = false;
            elan.UpdateAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Elan elan = await _context.Elans
                .Include(b => b.Category)
                .Include(e=> e.ElanTags)
                .ThenInclude(et=> et.Tag)
                .FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);

            if (elan == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(elan);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Elan elan = await _context.Elans.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (elan == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");

            }



            if (elan.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }





            elan.IsDeleted = true;
            elan.DeletedAt = DateTime.UtcNow.AddHours(4);
            elan.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index", new { status = 200 });
        }



    }
}
