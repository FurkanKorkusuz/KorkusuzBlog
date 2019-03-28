using Blog.BLL.Repository;
using Blog.Entity.Entity;
using Korkusuz.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Korkusuz.PL.Controllers
{
    public class HomeController : BaseController
    {

        BlogContext ent = new BlogContext();
        //Repository<Article> repoM = new Repository<Article>(new BlogContext());
        Repository<Tag> repoT = new Repository<Tag>(new BlogContext());
        Repository<Category> repoC = new Repository<Category>(new BlogContext());
        Repository<Comment> repoComment = new Repository<Comment>(new BlogContext());
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
   
        public ActionResult Blog(int Page=1)
        {
            var makale = ent.Articles.Where(m=>m.IsDeleted==false).OrderBy(m=>m.OkunmaSayisi).ToPagedList(Page,6);
            return View(makale);
        }
        public ActionResult KategoriPartial()
        {
            return View(ent.Categories.Where(c => c.IsDeleted==false).ToList());
        }
        public ActionResult MakalelerByKategori(int? Id)
        {
            if (Id == null)
            {
                Response.Redirect("/Home/Blog");
            }
            var makale = ent.Articles.Where(m => m.CategoryId == Id && m.IsDeleted == false).ToList();
            return View(makale); 
        }
        public ActionResult MakalelerByEtiket(int? etiketId)
        {
            return View(repoT.Get(e => e.Id == etiketId).Articles);
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Portofilo()
        {
            return View();
        }
        public ActionResult Servisler()
        {
            return View();
        }
   
        public ActionResult Article(int? id)
        {
          
            var makale = ent.Articles.Where(m => m.Id == id).SingleOrDefault();
            if (makale == null)
            {
                Response.Redirect("/Home/Blog");
            }

            return View(makale);
        }
        public JsonResult Yorum(string yorum, int makaleId)
        {
            Comment c = new Comment();
            var kullanici = Session["UserId"];
            if (yorum == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            c.UserId = Convert.ToString(kullanici);
            c.ArticleId = makaleId;
            c.Content = yorum;
            c.CreatedDate = DateTime.Now;
            repoComment.Add(c);
            //ent.Comments.Add(new Comment { UserId = Convert.ToString(kullanici), ArticleId = makaleId, Content = yorum, CreatedDate = DateTime.Now });
            //ent.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteComment(int id)
        {
            var kullanici = Session["UserId"];
            var yorum = ent.Comments.Where(y => y.Id == id).SingleOrDefault();
            var makale = ent.Articles.Where(m => m.Id == yorum.ArticleId).SingleOrDefault();
            if (Convert.ToString(kullanici)==yorum.UserId)
            {
                yorum.IsDeleted = true;
                ent.SaveChanges();
                return RedirectToAction("Article", "Home", new { id = makale.Id });
            }
            return HttpNotFound();
        }
        public ActionResult OkunmaArttır(int id)
        {
            var makale = ent.Articles.Where(m => m.Id == id).SingleOrDefault();
            makale.OkunmaSayisi += 1;
            ent.SaveChanges();
            return View();
        }
    }
}