using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itreansition.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Itreansition.Controllers
{
    public class LoginController : Controller
    {
        private AppDbContext db;
        public LoginController(AppDbContext context)
        {
            db = context;
        }


    }

}