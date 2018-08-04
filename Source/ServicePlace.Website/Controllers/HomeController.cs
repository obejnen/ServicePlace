﻿using System.Collections.Generic;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces;

namespace ServicePlace.Website.Controllers
{
    public class HomeController : Controller
    {

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