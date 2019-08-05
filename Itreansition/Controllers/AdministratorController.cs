using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Itreansition.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministratorController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        public AdministratorController(RoleManager<IdentityRole> manager)
        {
            _roleManager = manager;
        }

        public IActionResult GetRoles()
        {
            return View(_roleManager.Roles.ToList());
        }
    }
}