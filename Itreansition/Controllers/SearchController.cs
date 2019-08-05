using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itreansition.Models;
using Itreansition.Services.SearchService;
using Itreansition.ViewModels;
using Korzh.EasyQuery.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Itreansition.Services.SearchService.Search;

namespace Itreansition.Controllers
{
    public class SearchController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private AppDbContext db;




        public SearchController(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            db = context;
        }


        [HttpPost]
        public ActionResult Index(string search)
        {
            if ((search != null) && (search != ""))
            {

                SearchViewModel result = new SearchViewModel();
                result.StringForSearch = search;
                result.FoundInComments= FindInComments(search, db);
                result.FoundInCompanies = FindInCompanies(search, db);
                result.FoundInBonuses = FindInBonuses(search, db);
                return View(result);


            }
            else return View();

        }


    }
}