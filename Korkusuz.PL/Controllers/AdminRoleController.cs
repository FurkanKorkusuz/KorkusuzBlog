using Blog.Entity.Identity;
using Korkusuz.DAL.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_IdentityOwin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminRoleController : Controller
    {
        private RoleManager<ApplicationRole> rolemanager;

        public AdminRoleController()
        {
            var roleStore = new RoleStore<ApplicationRole>(new BlogContext());
            rolemanager = new RoleManager<ApplicationRole>(roleStore);
        }
        public ActionResult Index()
        {
            return View(rolemanager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationRole model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var rol = rolemanager.FindByName(model.Name);
            if (rol != null)
            {
                ModelState.AddModelError("", "Bu rol sistemde kayıtlı!");
                return View(model);
            }
            var result = rolemanager.Create(model);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }
    }
}