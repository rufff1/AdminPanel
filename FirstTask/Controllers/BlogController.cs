using FirstTask.DAL;
using FirstTask.Extensions;
using FirstTask.Helpers;
using FirstTask.Models;
using FirstTask.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstTask.Controllers
{



    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }




        public IActionResult Index(int pageIndex)
        {
       
            IQueryable<Blog> blogs = _context.Blogs
                .Include(e => e.category)
                .Include(b=> b.BlogTags)
                .ThenInclude(bt=> bt.Tag)
                .Where(b => b.IsDeleted == false);
            return View(PageNationList<Blog>.Create(blogs, pageIndex, 3));

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
        public async Task<IActionResult> Create(Blog blog)
        {

            ViewBag.Category = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tag = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();


            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!await _context.Categories.AnyAsync(c => c.IsDeleted == false && c.Id == blog.CategoryId))
            {
                ModelState.AddModelError("blog.CategoryId", "Gelen Category yalnisdir");
                return View(blog);
            }

            List<BlogTag> blogTags = new List<BlogTag>();

            foreach (int tagId in blog.TagIds)
            {
                if (blog.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    ModelState.AddModelError("TagIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(blog);

                }

                if (!await _context.Tags.AnyAsync(t => t.IsDeleted == false && t.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "secilen tag yalnisdir");
                    return View(blog);
                }

                BlogTag blogTag = new BlogTag
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    TagId = tagId

                };

              
                blogTags.Add(blogTag);
            }

            if (blog.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!blog.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!blog.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }
            blog.Image = blog.ImageFile.CreateImage(_env, "manage", "assets", "img", "Blog-photo");
            blog.Title = blog.Title;
            blog.Description = blog.Description;
            blog.BlogTags = blogTags;
            blog.CreatBy = "System";
            blog.IsDeleted = false;
            blog.CreatAt = DateTime.UtcNow.AddHours(4);
            await _context.Blogs.AddAsync(blog);
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
            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);

            if (blog == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            blog.TagIds = await _context.BlogTags.Where(pt => pt.BlogId == id).Select(x => x.TagId).ToListAsync();
           

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Blog blog)
        {
            ViewBag.Category = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tag = await _context.Tags.Where(t => t.IsDeleted == false).ToListAsync();

            if (!ModelState.IsValid) return View();

            if (id == null) return BadRequest("Id daxil edin");

            if (blog.Id != id)
            {
                return BadRequest("Id yalnisdir");
            }
            Blog existedBlog = await _context.Blogs
                .Include(t=> t.BlogTags)
               
                .FirstOrDefaultAsync(e => e.IsDeleted == false && e.Id == id);

            if (existedBlog == null)
            {
                return NotFound("Bele bir melumat yoxdur. Id yalnisdir");
            }



            _context.BlogTags.RemoveRange(existedBlog.BlogTags);

            List<BlogTag> blogTags = new List<BlogTag>();

            foreach (int tagId in blog.TagIds)
            {
                if (blog.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    ModelState.AddModelError("TagIds", "bir tagdan yalniz bir defe secilmelidir");
                    return View(blog);

                }

                if (!await _context.Tags.AnyAsync(t => t.IsDeleted == false && t.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "secilen tag yalnisdir");
                    return View(blog);
                }

                BlogTag blogTag = new BlogTag
                {
                    CreatAt = DateTime.UtcNow.AddHours(+4),
                    CreatBy = "System",
                    IsDeleted = false,
                    TagId = tagId

                };


                blogTags.Add(blogTag);
            }


            if (blog.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!blog.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!blog.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }

            Helper.DeleteFile(_env, existedBlog.Image, "manage", "assets", "img", "Blog-photo");
            existedBlog.Image = blog.ImageFile.CreateImage(_env, "manage", "assets", "img", "Blog-photo");
            existedBlog.Title = blog.Title;
            existedBlog.Description = blog.Description;
            existedBlog.BlogTags = blogTags;
            blog.UpdateBy = "System";
            blog.IsDeleted = false;
            blog.UpdateAt = DateTime.UtcNow.AddHours(4);

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

            Blog blog = await _context.Blogs
                .Include(b => b.category)
                .Include(b=> b.BlogTags)
                 .ThenInclude(bt=> bt.Tag)
                .FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);

            if (blog == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Blog blog = await _context.Blogs.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (blog == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");

            }



            if (blog.Id != id)
            {
                return BadRequest("Id bos ola bilmez");
            }





            blog.IsDeleted = true;
            blog.DeletedAt = DateTime.UtcNow.AddHours(4);
            blog.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index", new { status = 200 });
        }
    }
}
