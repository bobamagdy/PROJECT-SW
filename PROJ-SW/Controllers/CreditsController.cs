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
    public class CreditsController : Controller
    {
        private databaseProEntities db = new databaseProEntities();

        // GET: Credits
        public ActionResult Index(int? id)
        {
            //var credits = db.Credits.Where(m => m.User_Id == id).ToList();
            var credits = db.Credits.Include(c => c.User);
            return View(credits);
        }

        // GET: Credits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit credit = db.Credits.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            return View(credit);
        }

        // GET: Credits/Create
        public ActionResult Create()
        {
            ViewBag.User_Id = new SelectList(db.Users, "user_id", "FName");
            return View();
        }

        // POST: Credits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Credit_id,Credit_Holder_Name,Credit_Expire,Postal_Code,CVV_Number,User_Id")] Credit credit)
        {
            if (ModelState.IsValid)
            {
                db.Credits.Add(credit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            ViewBag.User_Id = new SelectList(db.Users, "user_id", "FName", credit.User_Id);
            return View(credit);
        }

        // GET: Credits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit credit = db.Credits.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.Users, "user_id", "FName", credit.User_Id);
            return View(credit);
        }

        // POST: Credits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Credit_id,Credit_Holder_Name,Credit_Expire,Postal_Code,CVV_Number,User_Id")] Credit credit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.Users, "user_id", "FName", credit.User_Id);
            return View(credit);
        }

        // GET: Credits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit credit = db.Credits.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            return View(credit);
        }

        // POST: Credits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Credit credit = db.Credits.Find(id);
            db.Credits.Remove(credit);
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
