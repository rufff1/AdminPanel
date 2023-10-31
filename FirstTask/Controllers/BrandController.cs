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
    public class BrandController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;


        public BrandController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int pageIndex)
        {
            IQueryable<Brand> brands = _context.Brands.Where(b => b.IsDeleted == false);

            return View(PageNationList<Brand>.Create(brands, pageIndex, 3));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }

            if (brand.Name == null)
            {
                ModelState.AddModelError("Name", "brand adi daxil edin");
                return View(brand);

            }


            if (await _context.Brands.AnyAsync(c => c.IsDeleted == false && c.Name.ToLower() == brand.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", $"This name {brand.Name} already exists");
                return View(brand);

            }

            if (brand.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!brand.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!brand.ImageFile.CheckFileType("image/png"))
            {
                ModelState.AddModelError("ImageFile", "image png tipinnen fayl secin! ");
                return View();
            }


            brand.Image = brand.ImageFile.CreateImage(_env,"manage", "assets", "img", "Brand-photo");
           
            brand.CreatBy = "System";
            brand.IsDeleted = false;
            brand.CreatAt = DateTime.UtcNow.AddHours(4);
            await _context.Brands.AddAsync(brand);
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

            Brand brand = await _context.Brands.FirstOrDefaultAsync(b=> b.IsDeleted == false && b.Id == id);


            if (brand == null)
            {
                return NotFound("Daxil etdiyiniz Id yalnisdir");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id , Brand brand)
        {
            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Brand existedBrand = await _context.Brands.FirstOrDefaultAsync(b=> b.IsDeleted == false && b.Id== id);

            if (existedBrand == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");

            }

            if (brand.Id != id)
            {
                return BadRequest("Id bos ola bilmez");

            }

            bool exist = await _context.Brands.AnyAsync(b => b.IsDeleted == false && b.Name.ToLower() == brand.Name.ToLower().Trim());

            if (exist && !((existedBrand.Name.ToLower() == brand.Name.ToLower().Trim())))
            {
                ModelState.AddModelError("", "Bu adda brand artig var");
                return View();
            }



            if (brand.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image daxil edin");
                return View();
            }

            if (!brand.ImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("ImageFile", "Image olcusu 1mb cox olmamalidir");
                return View();
            }
            if (!brand.ImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("ImageFile", "image jpeg tipinnen fayl secin! ");
                return View();
            }
            Helper.DeleteFile(_env, existedBrand.Image, "manage", "assets", "img", "Brand-photo");
            existedBrand.Image = brand.ImageFile.CreateImage(_env, "manage", "assets", "img", "Brand-photo");
            existedBrand.Name = brand.Name;
            existedBrand.BrandInfo = brand.BrandInfo;
            brand.IsDeleted = false;
            brand.UpdateBy = "System";
            brand.UpdateAt = DateTime.UtcNow.AddHours(4);


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


            Brand brand = await _context.Brands.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (brand == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            return View(brand);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            Brand brand = await _context.Brands.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (brand == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            if (brand.Id != id)
            {
                return BadRequest(" Id yalnisdir");
            }




            brand.IsDeleted = true;
            brand.DeletedAt = DateTime.UtcNow.AddHours(4);
            brand.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

    }
}
