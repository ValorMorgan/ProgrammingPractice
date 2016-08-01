using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using ProgrammingPractice.Service;
using ProgrammingPractice.Hubs;
using System.Reflection;

namespace ProgrammingPractice.Controllers.Ajax_Controllers
{
    public class MultiThreadAjaxController : Controller
    {
        private object ajaxSuccess = new { success = true, message = string.Empty };
        private Page currentPage {
            get
            {
                //Page page = System.Web.HttpContext.Current.Handler as Page;
                var page = new Page();
                var requestField = typeof(Page).GetField("_request", BindingFlags.Instance | BindingFlags.NonPublic);
                requestField.SetValue(page, System.Web.HttpContext.Current.Request);

                if (page == null)
                    throw new HttpException("Current Page object could not be found.");
                return page;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SingleThreadAsync()
        {
            try
            {
                await Task.Run(() => MultiThreadService.SingleThread());
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HttpContext.Response.TrySkipIisCustomErrors = true;
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message));
            }

            return Json(ajaxSuccess);
        }

        [HttpPost]
        public async Task<JsonResult> MultiThreadAsyc()
        {
            try
            {
                await Task.Run(() => MultiThreadService.MultiThread());
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