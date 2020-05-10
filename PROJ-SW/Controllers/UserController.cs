using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROJ_SW.Model;

namespace PROJ_SW.Controllers
{
    public class UserController : Controller
    {
        databaseProEntities db = new databaseProEntities();
        public ActionResult Index()
        {


            return View();
        }





        public ActionResult UserRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserRegistration(User userdet)
        {
            Login log = new Login();
            log.Login_Id = 2;
            log.Email = userdet.Email;
            log.Password = userdet.Password;
            log.Isdeleted = false;
            log.CreatedOn = DateTime.Today.Date;
            log.Role_Id = 2;


            db.Logins.Add(log);
            db.SaveChanges();

            userdet.Login_Id = db.Logins.Max(a => a.Login_Id);
            db.Users.Add(userdet);
            db.SaveChanges();

            return RedirectToAction("UserRegistration");
        }
        //[Authorize(Roles ="User")]

    }
}