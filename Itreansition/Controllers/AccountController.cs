using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dropbox.Api;
using EmailApp.ViewModels;
using Itreansition.Models;
using Itreansition.Services.HomeSevice;
using Itreansition.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Itreansition.Services.HomeSevice.FunctionHelpers;
namespace Itreansition.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private AppDbContext db;
        //public DropboxClient _dbx;




        public AccountController(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager/*, DropboxClient dbx*/)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            db = context;
            //_dbx = dbx;
        }

        public async Task<IActionResult> Index(string user)
        {
            if (_userManager.FindByNameAsync(user)!=null)
            {
                AccountViewModels AccountModel = new AccountViewModels();
                AccountModel.AccountUser = await _userManager.FindByNameAsync(user);
                AccountModel.ListCompany = db.Companies.Where(u => u.OwnerName == user);
                AccountModel.AchievementUser = db.Achievements.FirstOrDefault(a => a.OwnerName == user);
                AccountModel.ListUserBonus = db.UserBonuses.Where(n => n.OwnerName == user);
                return View(AccountModel);
            }
            return RedirectToAction("Index", "Home");

        }



        //------------------------------------------------Registration
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.UserName };
                user.DateCheck = DateTime.Now;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailSender emailService = new EmailSender();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                    $"Confirm registration by clicking on the link: <a href='{callbackUrl}'>link</a>");

                    return Redirect("/Home/Message?message=To complete the registration, check the email and follow the link provided in the letter");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        //------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null) return View("Error");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                Achievement achievement = new Achievement();
                achievement.OwnerName = user.UserName;
                db.Achievements.Add(achievement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else return View("Error");
        }

        //-----------------------------------------------------------Login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Email not confirmed");
                        return View(model);
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded) return RedirectToAction("Index", "Home");
                else ModelState.AddModelError("", "Incorrect username and (or) password");



            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult Setting(string user)
        {
            if ((User.Identity.Name == user) || (User.IsInRole("admin")))
            {
                User userModel = db.Users.FirstOrDefault(u => u.UserName == user);
                return View(userModel);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]

        public async Task<IActionResult> Setting(IFormFile LoadedFile, UserSettingViewModel editedUser)
        {

            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(n=>n.UserName== editedUser.UserName);
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;
                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, editedUser.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, editedUser.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return Redirect("~/Account?user=" + user.UserName);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else ModelState.AddModelError(string.Empty, "User not found");
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> ChangeAvatar(IFormFile LoadedFile,string userName)
        {
            if(LoadedFile!=null)
            {
                User user =db.Users.FirstOrDefault(u => u.UserName == userName);
                if (await SaveImgAsync(LoadedFile, "Avatar", userName))
                {
                    user.Avatar = GetPathImg(LoadedFile.FileName, "Avatar", userName);
               
                    db.Users.Update(user);
                await db.SaveChangesAsync();
                }
            }
            return Redirect("~/Account?user=" + userName);
        }

        [HttpPost]
        public async Task<IActionResult> AddMoney(string userName)//for test
        {
            User user = await db.Users.FirstOrDefaultAsync(i => i.UserName == userName);
            user.Money += 10000000;
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return Redirect("/Home/Message?message=Account replenished");
        }
    }
}

