using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Itreansition.Models;
using Microsoft.EntityFrameworkCore;
using Itreansition.Services.CompanyService;
using Itreansition.ViewModels;

namespace Itreansition.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db;
        public HomeController(AppDbContext context)
        {
            db = context;

        }

        public async Task<IActionResult> Index()  
        {
            ViewBag.TopList = await db.Companies.ToListAsync();
            ListCompanyViewModel listCompanyViewModel = new ListCompanyViewModel();
            listCompanyViewModel.NewCompaniesList = SortCompanyService.SortNew(db);
            listCompanyViewModel.TopCompaniesList = SortCompanyService.SortRich(db);
            return View(listCompanyViewModel);
        }                                      

        public IActionResult Create()      
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            Company company = new Company();
            company.Name = "Test";
            company.DateCreate = DateTime.Now;
            user.DateCheck= DateTime.Now; ;
            db.Users.Add(user);
            db.Companies.Add(company);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }                             

        [HttpPost]
        public async Task<IActionResult> Edit(User phone)
        {
            db.Users.Update(phone);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }                                       
        public IActionResult Message(string message)
        {
            ViewBag.message = message;
            return View();
        }


        public async Task<IActionResult> All()
        {
            IEnumerable<Company> companies = db.Companies;
            return View(companies);
        }

       

    }
}
