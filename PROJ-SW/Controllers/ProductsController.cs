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
    public class ProductsController : Controller
    {
        private databaseProEntities db = new databaseProEntities();
       
        
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Inventory);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Cate_Id = new SelectList(db.Categories, "cate_id", "cate_name");
            ViewBag.inventory_id = new SelectList(db.Inventories, "inventory_id", "inventory_name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product,HttpPostedFileBase Prod_Image)
        {
            if (ModelState.IsValid)
                {
                String path = "";
                if (Prod_Image.FileName.Length > 0)
                {
                    path = "~/Images/" + Path.GetFileName(Prod_Image.FileName);
                    Prod_Image.SaveAs(Server.MapPath(path));
                }
                product.Prod_Image = path;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cate_Id = new SelectList(db.Categories, "cate_id", "cate_name", product.Cate_Id);
            ViewBag.inventory_id = new SelectList(db.Inventories, "inventory_id", "inventory_name", product.inventory_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cate_Id = new SelectList(db.Categories, "cate_id", "cate_name", product.Cate_Id);
            ViewBag.inventory_id = new SelectList(db.Inventories, "inventory_id", "inventory_name", product.inventory_id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prod_id,prod_name,Price,Prod_Image,Description,MGF_Date,Expiry_Date,Batch_No,Cate_Id,inventory_id")] Product product, HttpPostedFileBase Prod_Image)
        {
            if (ModelState.IsValid)
            {
                String path = "";
                if (Prod_Image.FileName.Length > 0)
                {
                    path = "~/Images/" + Path.GetFileName(Prod_Image.FileName);
                    Prod_Image.SaveAs(Server.MapPath(path));
                }
                product.Prod_Image = path;

                db.Products.Add(product);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cate_Id = new SelectList(db.Categories, "cate_id", "cate_name", product.Cate_Id);
            ViewBag.inventory_id = new SelectList(db.Inventories, "inventory_id", "inventory_name", product.inventory_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
             db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ProductPage()
        {
            var recs = db.Products.ToList();
            return View(recs);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult HomePage()
        {

            var res = db.Products.ToList();

            return View(res);
        }
    }
}
