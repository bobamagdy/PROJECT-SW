using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJ_SW.Controllers
{
    public class ArticalController : Controller
    {
        // GET: Artical
        public ActionResult All()
        {
            ViewBag.Message = "Your All page.";

            return View();
        }
        public ActionResult Artical1()
        {
            ViewBag.Message = "Your Artical1 page.";

            return View();
        }
        public ActionResult Artical2()
        {
            ViewBag.Message = "Your Artical2 page.";

            return View();
        }
        public ActionResult Artical3()
        {
            ViewBag.Message = "Your Artical3 page.";

            return View();
        }
    }
}