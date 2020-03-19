using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJ_SW.Model;
using System.Web.Security;

namespace PROJ_SW.Controllers
{
    public class AccountController : Controller
    {

        databaseProEntities db = new databaseProEntities();
        public ActionResult Login()
        {
            return View();
            ViewBag.Message = "";

        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password")]Login log)
        {
            var result = db.Logins.Where(x => x.Email == log.Email && x.Password == log.Password).ToList();
            if (result.Count() > 0)
            {
                Session["Login_Id"] = result[0].Login_Id;
                FormsAuthentication.SetAuthCookie(result[0].Email, false);
                if (result[0].Role_Id == 1)
                {
                    return RedirectToAction("../Admin/Index");
                }
                else if (result[0].Role_Id == 2)
                {
                    return RedirectToAction("https://localhost:44344/Home/index");
                }
            }
            else
            {
                ViewBag.Message = "Incorrect Email or Password";
            }
            return View(log);
        }





        public ActionResult Logout()
        {
            Session["Login_Id"] = 0;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}