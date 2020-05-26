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
    public class CartsController : Controller
    {
        private databaseProEntities db = new databaseProEntities();

        // GET: Carts
        public ActionResult Index()
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
            return View(db.Products.OrderByDescending(x => x.prod_id).ToList());
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




            return RedirectToAction("Index");
        }
    }
}
