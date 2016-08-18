using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;

namespace ProgrammingPractice.UI.MVC.Controllers.Ajax_Controllers
{
    /// <summary>
    /// Callable controller from any Ajax calls to perform
    /// Ajax specifc operations in regards to ClientInformation.
    /// </summary>
    public class ClientInformationAjaxController : Controller
    {
        private object ajaxSuccess = new { success = true, message = string.Empty };

        /// <summary>
        /// Generates a Guid for the Client to hold in localStorage.
        /// </summary>
        /// <returns>Success alongside the Guid value.</returns>
        [HttpGet]
        public JsonResult GenerateClientId()
        {
            object data;

            try
            {
                data = new { success = true, message = string.Empty, clientId = Guid.NewGuid() };
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HttpContext.Response.TrySkipIisCustomErrors = true;
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message));
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}