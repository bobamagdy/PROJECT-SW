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
        public ActionResult Create(Product product, HttpPostedFileBase Prod_Image)
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
                //////////////////////////////////heba////////////////////////////////////////////////////
                return RedirectToAction("Index");
                //////////////////////////////////heba////////////////////////////////////////////////////
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
            TempData["imgpath"] = product.Prod_Image;
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

        public ActionResult Edit([Bind(Include = "prod_id,prod_name,Price,Prod_Image,Description,MGF_Date,Expiry_Date,Batch_No,Cate_Id,inventory_id")] Product product, HttpPostedFileBase imgFile)
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
                    product.Prod_Image = path;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    product.Prod_Image = TempData["imgpath"].ToString();
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

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
        public ActionResult ProductPage(string searchString)
        {


            var products = from s in db.Products
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Category.cate_name.Contains(searchString));
            }
            var categories = products.OrderBy(p => p.Category.cate_name).Select(p => p.Category.cate_name).Distinct();
            ViewBag.Category = new SelectList(categories);
            return View(products.ToList());

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult HomePage(int? id)
        {

            //var res = db.Products.ToList();

            //return View(res);
            var res = db.Products.Where(m => m.Cate_Id == id).ToList();
            return View(res);
        }
        public ActionResult viewProductUSER(int? id)
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




        public ActionResult AddToCart(int? Id)
        {

            Product p = db.Products.Where(x => x.prod_id == Id).SingleOrDefault();
            return View(p);
        }

        List<Cart> li = new List<Cart>();
        [HttpPost]
        public ActionResult AddToCart(Product pi, string qty, int Id)
        {
            Product p = db.Products.Where(x => x.prod_id == Id).SingleOrDefault();

            Cart c = new Cart();
            c.prod_id = p.prod_id;
            c.price = (float)p.Price;
            c.quan = Convert.ToInt32(qty);
            c.bill = c.price * c.quan;
            c.prod_name = p.prod_name;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;

            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                li2.Add(c);
                TempData["cart"] = li2;
            }

            TempData.Keep();




            return RedirectToAction("ProductPage");
        }

        private int IsExistingCheck(int? id)
        {
            List<Cart> IsCart = TempData["cart"] as List<Cart>;

            for (int i = 0; i < IsCart.Count; i++)
            {
                if (IsCart[i].prod_id == id)
                    return i;
            }
            return -1;
        }

        public ActionResult DeleteItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(id);
            List<Cart> IsCart = TempData["cart"] as List<Cart>;
            IsCart.RemoveAt(check);
            return View("checkout");
        }
        public ActionResult checkout()
        {
            if (TempData["cart"] != null)
            {
                float x = 0;
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += (float)item.bill;

                }

                TempData["total"] = x;
            }
            TempData.Keep();
            return View();
        }
       
    }
}
