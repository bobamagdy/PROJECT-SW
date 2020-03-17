using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJ_SW.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


    }
}