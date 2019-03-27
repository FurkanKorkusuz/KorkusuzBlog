using Blog.BLL.Repository;
using Blog.Entity.Entity;
using Korkusuz.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Korkusuz.PL.Controllers
{
    public class BaseController : Controller
    {
        BlogContext ent = new BlogContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Repository<Category> repoC = new Repository<Category>(ent);
            ViewBag.Kategoriler = repoC.GetAll();

            Repository<Tag> repoT = new Repository<Tag>(ent);
            ViewBag.Etiketler = repoT.GetAll(null, t => t.OrderByDescending(x => x.Articles.Count)).Take(5);

            base.OnActionExecuting(filterContext);
        }

    }
}