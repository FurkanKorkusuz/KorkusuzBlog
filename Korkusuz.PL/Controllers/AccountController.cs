using Blog.Entity.Identity;
using Blog.Entity.ViewModel;
using Korkusuz.DAL.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_IdentityOwin.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> usermanager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new BlogContext());
            usermanager = new UserManager<ApplicationUser>(userStore);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]    //Herkes giriş yapabilir.
        //[Authorize]       //Sadece login olanlar girebilir.
        //[Authorize(Roles ="Admin")] //Sadece login olanlardan Admin rolünde olanlar girebilir.
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            //var kullanici = usermanager.FindByName(model.Username);
            var kullanici = usermanager.FindByEmail(model.Email);
            if(kullanici != null)
            {
                ModelState.AddModelError("", "Bu email sistemde kayıtlı!");
                return View(model);
            }
            ApplicationUser user = new ApplicationUser();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.Username;
            Session["UserId"] = user.Id;
            Session["UserName"] = user.UserName;
            

            var result = usermanager.Create(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //ViewBag.returnUrl = returnUrl;
            LoginViewModel model = new LoginViewModel() { returnUrl = returnUrl };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var userStore = new UserStore<ApplicationUser>(new BlogContext());
            var usermanager = new UserManager<ApplicationUser>(userStore);
            var kullanici = usermanager.FindByName(model.Username);
            if (kullanici == null)
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı kayıtlı değil!");
                return View(model);
            }
            else
            {
                var authManager = HttpContext.GetOwinContext().Authentication;
                var identity = usermanager.CreateIdentity(kullanici, "ApplicationCookie");
                var authProperty = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };
                authManager.SignIn(authProperty, identity);
                Session["UserId"] = kullanici.Id;
                Session["UserName"] = kullanici.UserName;
                Session["YetkiId"] = kullanici.Yetki;

                return Redirect(string.IsNullOrEmpty(model.returnUrl) ? "/" : model.returnUrl);
            }
        }
        //[Authorize]
        [HttpPost]
        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
         
            Session["UserName"] = null;
            Session["UserId"] = null;          
            Session["YetkiId"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}