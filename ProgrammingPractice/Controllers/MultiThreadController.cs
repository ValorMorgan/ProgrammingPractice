using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgrammingPractice.Controllers
{
    public class MultiThreadController : Controller
    {
        // GET: MultiThread
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the MultiThreading Example.";

            return View();
        }
    }
}