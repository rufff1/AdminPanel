using FirstTask.DAL;
using FirstTask.Extensions;
using FirstTask.Helpers;
using FirstTask.Interfaces;
using FirstTask.Models;
using FirstTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FirstTask.Controllers
{

    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        // private readonly IEmailService _emailService;
        private readonly IFileService _fileService;


        public AccountController(IFileService fileService, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env, AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _context = context;
            // _emailService = emailService;
            _fileService = fileService;

        }
        [Authorize(Roles = "HR,Manager")]
        public IActionResult Index()
        {

            return View();
        }



        [HttpGet]
        public async Task< IActionResult> Register()
        {
           ViewBag.State = await _context.States.Where(c => c.IsDeleted == false).ToListAsync();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM )
        {
            ViewBag.State = await _context.States.Where(c => c.IsDeleted == false).ToListAsync();


            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            if (!await _context.States
             
           
                .AnyAsync(s => s.IsDeleted == false && s.Id == registerVM.AppUser.StateId))
            {
                ModelState.AddModelError("registerVM.StateId", "Gelen State yalnisdir");
                return View(registerVM);
            }

         
            AppUser appUser = new AppUser
            {
                Name = registerVM.Name,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                Job = registerVM.Job,
                Salary =registerVM.Salary,
                Phone = registerVM.Phone,
                Adress = registerVM.Adress,
             

            };

           

            if (registerVM.Salary >= 350)
            {

                appUser.Salary = registerVM.Salary;

            }
            else
            {
                ModelState.AddModelError("Salary", "Minimum emek haqqi 350-azndir");
                return View(registerVM);
            }




            if (registerVM.UserImageFile == null)
            {
                ModelState.AddModelError("UserImageFile", "Profile şəklini yükləməyiniz tələb olunur");
                return View();
            }
            if (!registerVM.UserImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("UserImageFile", "Seçilmiş faylın tipi jpeg olmalıdır");
                return View();
            }
            if (!registerVM.UserImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("UserImageFile", "Secilmiş şəklin həcmi 1000KB-dan artıq ola bilməz");
                return View();
            }
            appUser.UserImageFile = registerVM.UserImageFile;
           
            appUser.UserImage = appUser.UserImageFile.CreateImage(_env, "manage", "assets", "img", "user-image");

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerVM.Paswoord);
            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registerVM);
            }

         

            await _userManager.AddToRoleAsync(appUser, "Member");
          
         
            return RedirectToAction("Login");

        }



          
 







    [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            AppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Email ve ya Paswoord duzgun qeyd edin");
                return View(loginVM);
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, loginVM.Paswoord, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Sifreni 3 defeden artig sehf yigdiginiz ucun bloklandiniz");
                return View(loginVM);
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email ve ya Paswoord duzgun qeyd edin");
                return View(loginVM);
            }
            await _signInManager.PasswordSignInAsync(appUser, loginVM.Paswoord, loginVM.RemindMe, true);
            return RedirectToAction("Index", "Dashboard");


        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {


           
            AppUser appUser = await _userManager
                .FindByNameAsync(User.Identity.Name);
            ProfileVM profileVM = new ProfileVM
            {
                Name = appUser.Name,
                UserName = appUser.UserName,
                Email = appUser.Email,
                Job = appUser.Job,
                Adress = appUser.Adress,
                Salary = appUser.Salary,
                Phone = appUser.Phone,
                UserImage = appUser.UserImage,
             

            };



            return View(profileVM);
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ProfileEdit()
        {
            ViewBag.State = await _context.States.Where(c => c.IsDeleted == false).ToListAsync();

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ProfileVM profileVM = new ProfileVM
            {
                Name = appUser.Name,
                UserName = appUser.UserName,
                Email = appUser.Email,
                Job = appUser.Job,
                Adress = appUser.Adress,
                Salary = appUser.Salary,
                Phone = appUser.Phone,
                UserImage = appUser.UserImage,
                 

            };

         
          

            return View(profileVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ProfileEdit(ProfileVM profileVM) 
        {
            ViewBag.State = await _context.States.Where(c => c.IsDeleted == false).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(profileVM);
            }
            bool check = false;

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);


        



            if (appUser.Name.ToLowerInvariant() != profileVM.Name.Trim().ToLowerInvariant())
            {
                check = true;
                appUser.Name = profileVM.Name.Trim();

            }
            if (appUser.NormalizedUserName != profileVM.UserName.Trim().ToUpperInvariant())
            {
                check = true;
                appUser.UserName = profileVM.UserName.Trim();

            }
            if (appUser.NormalizedEmail != profileVM.Email.Trim().ToLowerInvariant())
            {
                check = true;
                appUser.Email = profileVM.Email.Trim();

            }
            if (appUser.Phone != profileVM.Phone.Trim().ToLowerInvariant())
            {
                check = true;
                appUser.Email = profileVM.Email.Trim();

            }
            profileVM.UserImage = appUser.UserImage;

            if (profileVM.UserImageFile == null)
            {
                ModelState.AddModelError("UserImageFile", " şəkil yükləməyiniz tələb olunur");
                return View();
            }
            if (!profileVM.UserImageFile.CheckFileType("image/jpeg"))
            {
                ModelState.AddModelError("UserImageFile", "Seçilmiş faylın tipi jpeg olmalıdır");
                return View();
            }
            if (!profileVM.UserImageFile.CheckFileSize(1000))
            {
                ModelState.AddModelError("UserImageFile", "Secilmiş şəklin həcmi 1000KB-dan artıq ola bilməz");
                return View();
            }
            if (appUser.UserImage != null)
            {
                Helper.DeleteFile(_env, appUser.UserImage, "manage", "assets", "img", "user-image");
            }
            appUser.UserImage = profileVM.UserImageFile.CreateImage(_env, "manage","assets", "img", "user-image");

            if (profileVM.Salary >= 350)
            {

                appUser.Salary = profileVM.Salary;

            }
            else
            {
                ModelState.AddModelError("Salary", "Minimum emek haqqi 350-azndir");
                return View(profileVM);
            }


         

            if (check)
            {
                appUser.UserName = profileVM.UserName;
                appUser.Job = profileVM.Job;
                appUser.Adress = profileVM.Adress;
                appUser.Salary = profileVM.Salary;
                appUser.Phone = profileVM.Phone;
              

                IdentityResult identityResult = await _userManager.UpdateAsync(appUser);
                if (!identityResult.Succeeded)
                {
                    foreach (var item in identityResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(profileVM);
                }
            }
            if (!string.IsNullOrWhiteSpace(profileVM.CurrentPaswoord))
            {
                if (!await _userManager.CheckPasswordAsync(appUser, profileVM.CurrentPaswoord))
                {
                    ModelState.AddModelError("CurrentPaswoord", "Sifrenizi duzgun daxil edin");
                    return View(profileVM);

                }
                if (profileVM.NewPaswoord == profileVM.CurrentPaswoord)
                {
                    ModelState.AddModelError("NewPaswoord", "Yeni Sifrenizle hal-hazirdaki eynidir");
                    return View(profileVM);

                }

                string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                IdentityResult identityResult = await _userManager.ResetPasswordAsync(appUser, token, profileVM.NewPaswoord);

                if (!identityResult.Succeeded)
                {
                    foreach (var item in identityResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(profileVM);

                }
            }

        



            return RedirectToAction("Index", "Dashboard");
        }







        // Paswoord reset with email
        // [HttpGet]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPassword)
        //{
        //    if (!ModelState.IsValid) return View();

        //    AppUser existUser = await _userManager.FindByEmailAsync(forgotPassword.Email);

        //    if (existUser == null)
        //    {
        //        ModelState.AddModelError("Email", "User not Found!");
        //        return View();
        //    }

        //    string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

        //    string link = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token },
        //        Request.Scheme, Request.Host.ToString());


        //    string body = string.Empty;
        //    string path = "wwwroot/manage/assets/templates/verify.html";
        //    string subject = "Verify password reset Email";

        //    body = _fileService.ReadFile(path, body);

        //    body = body.Replace("{{link}}", link);
        //    body = body.Replace("{{FullName}}", existUser.Name);

        //    _emailService.Send(existUser.Email, subject, body);

        //    return RedirectToAction(nameof(VerifyEmail));
        //}



        //[HttpGet]
        //public IActionResult ResetPassword(string userId, string token)
        //{
        //    return View(new ResetPasswordVM { UserId = userId, Token = token });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        //{
        //    if (!ModelState.IsValid) return View(resetPassword);

        //    AppUser existUser = await _userManager.FindByIdAsync(resetPassword.UserId);

        //    if (existUser == null) return NotFound();

        //    if (await _userManager.CheckPasswordAsync(existUser, resetPassword.Password))
        //    {
        //        ModelState.AddModelError("", "Your password already exist!");
        //        return View(resetPassword);
        //    }


        //    await _userManager.ResetPasswordAsync(existUser, resetPassword.Token, resetPassword.Password);

        //    return RedirectToAction("Login", "Account");
        //}


        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{
        //    if (userId == null || token == null) return BadRequest();

        //    AppUser user = await _userManager.FindByIdAsync(userId);

        //    if (user == null) return NotFound();

        //    await _userManager.ConfirmEmailAsync(user, token);

        //    await _signInManager.SignInAsync(user, false);

        //    return RedirectToAction("Index", "Home");
        //}


        //public IActionResult VerifyEmail()
        //{
        //    return View();
        //}



        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "HR" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Manager" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });

        //    return Ok();
        //}


        //public async Task<IActionResult> CreateHR()
        //{
        //    AppUser appUser = new AppUser
        //    {
        //        Email = "rr.rufff@code.az",
        //        Name = "Rufat",
        //        UserName = "rufff"
        //    };
        //    appUser.EmailConfirmed = true;
        //    //AppUser appUser = await _userManager.FindByEmailAsync("rr.rufff@code.az");

        //    await _userManager.CreateAsync(appUser, "Rufff123");
        //    //await _userManager.AddPasswordAsync(appUser,"Rufff123");
        //    await _userManager.AddToRoleAsync(appUser, "HR");

        //    return Ok();
        //}


        //public async Task<IActionResult> CreateManager()
        //{
        //    AppUser appUser = new AppUser
        //    {
        //        Email = "ismayil@mail.ru",
        //        Name = "isi",
        //        UserName = "isi"
        //    };
        //    appUser.EmailConfirmed = true;
        //    //AppUser appUser = await _userManager.FindByEmailAsync("rr.rufff@code.az");

        //    await _userManager.CreateAsync(appUser, "Rufff123");
        //    //await _userManager.AddPasswordAsync(appUser,"Rufff123");
        //    await _userManager.AddToRoleAsync(appUser, "Manager");

        //    return Ok();
        //}


    }
}
