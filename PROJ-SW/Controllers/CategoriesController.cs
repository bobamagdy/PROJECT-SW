using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROJ_SW.Model;
using System.IO;

namespace PROJ_SW.Controllers
{
    public class CategoriesController : Controller
    {
        private databaseProEntities db = new databaseProEntities();
        private bool id;

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cate_id,cate_name,Cate_Image")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            TempData["imgpath"] = category.Cate_Image;
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = db.Categories.Find(id);
        //    TempData["imgpath"] = category.Cate_Image;
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cate_id,cate_name,Cate_Image")] Category category, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                if (imgFile != null)
                {
                    String path = "";
                    if (imgFile.FileName.Length > 0)
                    {
                        path = "~/Images/" + Path.GetFileName(imgFile.FileName);
                        imgFile.SaveAs(Server.MapPath(path));
                    }
                    category.Cate_Image = path;
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    category.Cate_Image = TempData["imgpath"].ToString();
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(category);
        }

        //public ActionResult Edit([Bind(Include = "cate_id,cate_name,Cate_Image")] Category category, HttpPostedFileBase file)
        //{
        //    String path = "";
        //    if (/*file.FileName.Length > 0*/file != null)
        //    {
        //        //path = "~/Images/" + Path.GetFileName(file.FileName);
        //        //file.SaveAs(Server.MapPath(path));
        //    }
        //    else
        //    {
        //        category.Cate_Image = TempData["imgpath"].ToString();
        //        db.Entry(category).State = EntityState.Modified;
        //        if (db.SaveChanges() > 0)
        //        {
        //            TempData["msg"] = "Data Updated successfully";
        //            ////////////////////////////////////////////heba/////////////////////////////////////////////////
        //            return RedirectToAction("Index");
        //            ///////////////////////////////////////////////////////////////heba//////////////////////
        //        }
        //    }
        //    return RedirectToAction("Index");
        //    //}
        //    return View(category);
        //}

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CategoryPage()
        {
            var recs = db.Categories.ToList();
            return View(recs);
        }


        public ActionResult ViewProduct(int? id)
        {
            var rrecss = db.Products.Where(m => m.Cate_Id == id).ToList();
            return View(rrecss);
        }

    }
}
