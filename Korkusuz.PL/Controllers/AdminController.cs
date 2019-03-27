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
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> usermanager;

        public AdminController()
        {
            var userStore = new UserStore<ApplicationUser>(new BlogContext());
            usermanager = new UserManager<ApplicationUser>(userStore);
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(usermanager.Users);
        }
    }
}