using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJ_SW.Controllers
{
    public class MedicinesController : Controller
    {
        // GET: Medicines
        public ActionResult Beauty()
        {
            return View();
        }
        public ActionResult Hair_care()
        {
            return View();
        }
        public ActionResult Medication()
        {
            return View();
        }
        public ActionResult Skin_care()
        {
            return View();
        }
        public ActionResult Supplements()
        {
            return View();
        }

    }
}