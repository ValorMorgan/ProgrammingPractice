using System;
using System.Net;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using ProgrammingPractice.Service;

namespace ProgrammingPractice.UI.MVC.Controllers.Ajax_Controllers
{
    /// <summary>
    /// Callable controller from any Ajax calls to perform
    /// Ajax specifc operations in regards to MulthThread.
    /// </summary>
    public class MultiThreadAjaxController : Controller
    {
        private object ajaxSuccess = new { success = true, message = string.Empty };
        private CancellationTokenSource singleThreadTokenSource;
        private CancellationTokenSource multiThreadTokenSource;

        /// <summary>
        /// Performs an operation over a single thread.  Used to compare
        /// single thread processing vs multithread processing.
        /// </summary>
        /// <returns>Success if passed.</returns>
        [HttpPost]
        public JsonResult SingleThread()
        {
            // ie singleThreadTokenSource_f1fd7650-fecf-4cee-952c-f4569698317c
            string cacheName = string.Format("singleThreadTokenSource_{0}", HttpContext.Request.Params["clientId"]);

            try
            {
                if (singleThreadTokenSource == null)
                {
                    singleThreadTokenSource = new CancellationTokenSource();
                    HttpContext.Cache[cacheName] = singleThreadTokenSource;
                }

                // Opening new Task with a Cancelation token
                Task task = Task.Run(() => {
                    MultiThreadService.SingleThread(singleThreadTokenSource.Token);
                }, singleThreadTokenSource.Token);

                // Don't finish this method until the Task is done
                // Otherwise UI updates as 100% complete.
                task.Wait();
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HttpContext.Response.TrySkipIisCustomErrors = true;
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message));
            }

            return Json(ajaxSuccess);
        }

        /// <summary>
        /// Cancels the single thread processing request.
        /// </summary>
        [HttpPost]
        public void CancelSingleThread()
        {
            // ie singleThreadTokenSource_f1fd7650-fecf-4cee-952c-f4569698317c
            string cacheName = string.Format("singleThreadTokenSource_{0}", HttpContext.Request.Params["clientId"]);

            if (singleThreadTokenSource == null)
                singleThreadTokenSource = HttpContext.Cache[cacheName] as CancellationTokenSource;

            singleThreadTokenSource.Cancel();
            (HttpContext.Cache[cacheName] as CancellationTokenSource).Dispose();
        }

        /// <summary>
        /// Performs an operation over multiple threads.  Used to compare
        /// single thread processing vs multithread processing.
        /// </summary>
        /// <returns>Success if passed.</returns>
        [HttpPost]
        public JsonResult MultiThread()
        {
            // ie multiThreadTokenSource_f1fd7650-fecf-4cee-952c-f4569698317c
            string cacheName = string.Format("multiThreadTokenSource_{0}", HttpContext.Request.Params["clientId"]);

            try
            {
                if (multiThreadTokenSource == null)
                {
                    multiThreadTokenSource = new CancellationTokenSource();
                    HttpContext.Cache[cacheName] = multiThreadTokenSource;
                }

                // Opening new Task with a Cancelation token
                Task task = Task.Run(() => {
                    MultiThreadService.MultiThread(multiThreadTokenSource.Token);
                }, multiThreadTokenSource.Token);

                // Don't finish this method until the Task is done
                // Otherwise UI updates as 100% complete.
                task.Wait();
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HttpContext.Response.TrySkipIisCustomErrors = true;
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message));
            }

            return Json(ajaxSuccess);
        }

        /// <summary>
        /// Cancels the multithread processing request.
        /// </summary>
        [HttpPost]
        public void CancelMultiThread()
        {
            // ie multiThreadTokenSource_f1fd7650-fecf-4cee-952c-f4569698317c
            string cacheName = string.Format("multiThreadTokenSource_{0}", HttpContext.Request.Params["clientId"]);

            if (multiThreadTokenSource == null)
                multiThreadTokenSource = HttpContext.Cache[cacheName] as CancellationTokenSource;

            multiThreadTokenSource.Cancel();
            (HttpContext.Cache[cacheName] as CancellationTokenSource).Dispose();
        }
    }
}