using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROJ_SW.Model;

namespace PROJ_SW.Controllers
{
    public class Cart_ProductController : Controller
    {
        private databaseProEntities db = new databaseProEntities();

        // GET: Cart_Product
        public ActionResult Index()
        {
            var cart_Product = db.Cart_Product.Include(c => c.Cart).Include(c => c.Product);
            return View(cart_Product.ToList());
        }

        // GET: Cart_Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_Product cart_Product = db.Cart_Product.Find(id);
            if (cart_Product == null)
            {
                return HttpNotFound();
            }
            return View(cart_Product);
        }

        // GET: Cart_Product/Create
        public ActionResult Create()
        {
            ViewBag.Cart_Id = new SelectList(db.Carts, "cart_id", "cart_id");
            ViewBag.Prod_Id = new SelectList(db.Products, "prod_id", "prod_name");
            return View();
        }

        // POST: Cart_Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cart_Id,Prod_Id")] Cart_Product cart_Product)
        {
            if (ModelState.IsValid)
            {
                db.Cart_Product.Add(cart_Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cart_Id = new SelectList(db.Carts, "cart_id", "cart_id", cart_Product.Cart_Id);
            ViewBag.Prod_Id = new SelectList(db.Products, "prod_id", "prod_name", cart_Product.Prod_Id);
            return View(cart_Product);
        }

        // GET: Cart_Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_Product cart_Product = db.Cart_Product.Find(id);
            if (cart_Product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cart_Id = new SelectList(db.Carts, "cart_id", "cart_id", cart_Product.Cart_Id);
            ViewBag.Prod_Id = new SelectList(db.Products, "prod_id", "prod_name", cart_Product.Prod_Id);
            return View(cart_Product);
        }

        // POST: Cart_Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cart_Id,Prod_Id")] Cart_Product cart_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cart_Id = new SelectList(db.Carts, "cart_id", "cart_id", cart_Product.Cart_Id);
            ViewBag.Prod_Id = new SelectList(db.Products, "prod_id", "prod_name", cart_Product.Prod_Id);
            return View(cart_Product);
        }

        // GET: Cart_Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_Product cart_Product = db.Cart_Product.Find(id);
            if (cart_Product == null)
            {
                return HttpNotFound();
            }
            return View(cart_Product);
        }

        // POST: Cart_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart_Product cart_Product = db.Cart_Product.Find(id);
            db.Cart_Product.Remove(cart_Product);
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
    }
}
