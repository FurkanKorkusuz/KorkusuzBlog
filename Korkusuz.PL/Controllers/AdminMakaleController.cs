using Blog.Entity.Entity;
using Korkusuz.DAL.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Korkusuz.PL.Controllers
{
    public class AdminMakaleController : Controller
    {
        BlogContext ent = new BlogContext();
        // GET: AdminMakale
        public ActionResult Index(int Page=1)
        {
            var makaleler = ent.Articles.OrderBy(m => m.OkunmaSayisi).ToPagedList(Page,3);
            return View(makaleler);
        }
      
        // GET: AdminMakale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminMakale/Create
        public ActionResult Create()
        {
            ViewBag.Kategori = ent.Categories;
            return View();
        }

        // POST: AdminMakale/Create

        [HttpPost,ValidateInput (false)]
        public ActionResult Create(Article makale, string etiketler, HttpPostedFileBase Foto )
        {
            ViewBag.Kategori = ent.Categories;
            try
            {
                if (Foto != null)
                {
                    //WebImage img = new WebImage(Foto.InputStream);
                    //img.FileName = Foto.FileName;
                    //FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string filename = Foto.FileName;
                    string imagePath = Server.MapPath("/Uploads/MakaleFoto/" + filename);
                    Foto.SaveAs(imagePath);
                    makale.Picture = "/Uploads/MakaleFoto/" + filename;

                    //string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    //img.Resize(800, 350);
                    //img.Save("/Uploads/MakaleFoto/" + newfoto);
                    //makale.Picture = "/Uploads/MakaleFoto/" + newfoto;
                }

                //if (etiketler != null)
                //{
                //    string[] etiketdizi = etiketler.Split(',');
                //    foreach (var i in etiketdizi)
                //    {
                //        var yeniEtiket = new Tag { Content = i };
                //        ent.Tags.Add(yeniEtiket);
                //        makale.Tags.Add(yeniEtiket);
                //    }
                //}

                ent.Articles.Add(makale);
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                string hata = ex.Message;
                return View();
            }
        }

        // GET: AdminMakale/Edit/5
        public ActionResult Edit(int? id)
        {
            var makale = ent.Articles.Where(m => m.Id == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kategori = ent.Categories;
            return View(makale);
        }

        // POST: AdminMakale/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, HttpPostedFileBase Foto, Article article)
        {

            var makale = ent.Articles.Where(m => m.Id == id).SingleOrDefault();
            ViewBag.Kategori = ent.Categories;
            try
            {
              
                //if (Foto != null)
                {
                    //if (System.IO.File.Exists(Server.MapPath(makale.Picture)))
                    //{
                    //    System.IO.File.Delete(Server.MapPath(makale.Picture));
                    //}

                    string filename = Foto.FileName;
                    string imagePath = Server.MapPath("/Uploads/MakaleFoto/" + filename);
                    Foto.SaveAs(imagePath);
                    makale.Picture = "/Uploads/MakaleFoto/" + filename;

                    //WebImage img = new WebImage(Foto.InputStream);
                    //FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    //string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    //img.Resize(800, 350);
                    //img.Save("/Uploads/MakaleFoto/" + newfoto);
                    //makale.Picture = "/Uploads/MakaleFoto/" + newfoto;

                    makale.Title = article.Title;
                    makale.Content = article.Content;
                    makale.Summary = article.Summary;
                    makale.CategoryId = article.CategoryId;
                    ent.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch 
            {
                return View();
            }
        }

        // GET: AdminMakale/Delete/5
        public ActionResult Delete(int id)
        {
            var makale = ent.Articles.Where(m => m.Id == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: AdminMakale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var makale = ent.Articles.Where(m => m.Id == id).SingleOrDefault();
                if (makale == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(makale.Picture)))
                {
                    System.IO.File.Delete(Server.MapPath(makale.Picture));
                }
                foreach (var i in makale.Comments.ToList())
                {
                    ent.Comments.Remove(i);
                }
                foreach (var i in makale.Tags.ToList())
                {
                    ent.Tags.Remove(i);
                }
                ent.Articles.Remove(makale);
                ent.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
