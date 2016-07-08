using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProgrammingPractice.Service;

namespace ProgrammingPractice.Controllers.Ajax_Controllers
{
    public class MultiThreadAjaxController : Controller
    {
        private object ajaxSuccess = new { success = true, message = string.Empty };

        public JsonResult SingleThread()
        {
            try
            {
                MultiThreadService.SingleThread();
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HttpContext.Response.TrySkipIisCustomErrors = true;
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message));
            }

            return Json(ajaxSuccess);
        }

        public JsonResult MultiThread()
        {
            try
            {
                MultiThreadService.MultiThread();
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HttpContext.Response.TrySkipIisCustomErrors = true;
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message));
            }

            return Json(ajaxSuccess);
        }
    }
}