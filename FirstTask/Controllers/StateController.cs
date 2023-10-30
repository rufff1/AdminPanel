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
    public class StateController : Controller
    {
        public readonly AppDbContext _context;

        public StateController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int pageIndex)
        {
            IQueryable<State> states = _context.States.Where(b => b.IsDeleted == false);

            return View(PageNationList<State>.Create(states, pageIndex, 3));
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(State state)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (await _context.States.AnyAsync(c => c.IsDeleted == false && c.Name.ToLower() == state.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("CounrtyName", $"Bu Country {state.Name} artig movcuddur");

                return View(state);
            }


            state.Name = state.Name.Trim();

            state.IsDeleted = false;
            state.CreatBy = "System";
            state.CreatAt = DateTime.UtcNow.AddHours(4);

            await _context.States.AddAsync(state);

            await _context.SaveChangesAsync();

            return Redirect("index");

        }


        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest("Id bos ola bilmez");
            }

            State state = await _context.States
                .Include(c => c.AppUsers)
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);


            if (state == null)
            {
                return NotFound("Daxil edilen Id yalnisdir");
            }

            if (state.Id != id)
            {
                return BadRequest(" Id yalnisdir");
            }


            if (state.AppUsers.Count() > 0)
            {
                return Json(new { status = 400 });
            }

            state.IsDeleted = true;
            state.DeletedAt = DateTime.UtcNow.AddHours(4);
            state.DeletedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction("index", new { status = 200 });
        }


        //[HttpGet]
        //public async Task<IActionResult> Update(int? id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest("Id bos ola bilme");
        //    }

        //    Country country = await _context.Countries.FirstOrDefaultAsync(c=> c.IsDeleted == false && c.Id == id);

        //    if (country == null)
        //    {
        //        return NotFound("Id yalnisdir");
        //    }
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(int? id,Country country)
        //{
        //    if (id== null)
        //    {
        //        return BadRequest();
        //    }

        //}
    }
}
