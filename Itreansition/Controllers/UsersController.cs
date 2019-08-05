using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itreansition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Itreansition.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(string name=null)
        {
            User user;

            if ((name != null) && (User.IsInRole("Admin"))) { user = await _userManager.FindByNameAsync(name);}
            else if (name != null) { return RedirectToAction("Index", "Home"); }
            else{user = await _userManager.FindByNameAsync(User.Identity.Name);}
            return View(user);
        }



    }

}