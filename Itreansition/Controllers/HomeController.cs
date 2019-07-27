using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Itreansition.Models;
using Microsoft.EntityFrameworkCore;

namespace Itreansition.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db;
        public HomeController(AppDbContext context)
        {
            db = context;

        }

        public async Task<IActionResult> Index()  //Вывод всех
        {
            ViewBag.TopList = await db.Companies.ToListAsync();

            return View();
        }                                      

        public IActionResult Create()      //Создание
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
        }                             //Создание

        [HttpPost]
        public async Task<IActionResult> Edit(User phone)
        {
            db.Users.Update(phone);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }                                        ////Редактирование



        //КОнец моей вставки

    }
}
