﻿using Misitu.RefTables.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Areas.Client.Controllers
{
    public class DashboardController : Controller
    {
     


        // GET: Client/Home
        public ActionResult Index()
        {
         
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
