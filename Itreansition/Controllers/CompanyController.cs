using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itreansition.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Itreansition.Controllers
{
    public class CompanyController : Controller
    {


        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private AppDbContext db;




        public CompanyController(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            db = context;
        }
        // GET: Companies
        public ActionResult Index()
        {
            return View();
        }



        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Create(Company company)
        {
                
            company.DateCreate = DateTime.Now;
            company.OwnerName = User.Identity.Name ;
            db.Companies.Add(company);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
         
            
        }

    }  
}