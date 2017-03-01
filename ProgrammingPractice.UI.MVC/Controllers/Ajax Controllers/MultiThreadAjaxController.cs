using System;
using System.Net;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using ProgrammingPractice.Interfaces;
using ProgrammingPractice.Service;
using ProgrammingPractice.UI.MVC.Services;
using System.Web;
using ProgrammingPractice.Error;


namespace ProgrammingPractice.UI.MVC.Controllers.Ajax_Controllers
{
    /// <summary>
    /// Callable controller from any Ajax calls to perform
    /// Ajax specifc operations in regards to MulthThread.
    /// </summary>
    public class MultiThreadAjaxController : Controller
    {
        #region VARIABLES
        private readonly IMultiThreadService _service = new MultiThreadService();

        private readonly object _ajaxSuccess = new { success = true, message = string.Empty };

        private CancellationTokenSource _singleThreadTokenSource;
        private CancellationTokenSource _multiThreadTokenSource;

        /// <summary>
        /// The single thread token source object.  Will create the cache if not found.
        /// </summary>
        private CancellationTokenSource SingleThreadTokenSource
        {
            get
            {
                if (_singleThreadTokenSource == null)
                {
                    string cacheName = SingleThreadCacheName(HttpContext.Request);

                    try
                    {
                        _singleThreadTokenSource = HttpContextCacheService.RetrieveCache(HttpContext.Cache, cacheName) as CancellationTokenSource;
                    }
                    catch (NullReferenceException ex)
                    {
                        // No cache found thus we create the cache for returning
                        System.Diagnostics.Debug.WriteLine(ex.Message + " - Creating new cache.");
                        _singleThreadTokenSource = new CancellationTokenSource();
                        HttpContext.Cache[cacheName] = _singleThreadTokenSource;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex);
                        throw;
                    }
                }

                return _singleThreadTokenSource;
            }
            set
            {
                _singleThreadTokenSource = value;
            }
        }

        /// <summary>
        /// The multithread token source object.  Will create the cache if not found.
        /// </summary>
        private CancellationTokenSource MultiThreadTokenSource
        {
            get
            {
                if (_multiThreadTokenSource == null)
                {
                    string cacheName = MultiThreadCacheName(HttpContext.Request);

                    try
                    {
                        _multiThreadTokenSource = HttpContextCacheService.RetrieveCache(HttpContext.Cache, cacheName) as CancellationTokenSource;
                    }
                    catch (NullReferenceException ex)
                    {
                        // No cache found thus we create the cache for returning
                        System.Diagnostics.Debug.WriteLine(ex.Message + " - Creating new cache.");
                        _multiThreadTokenSource = new CancellationTokenSource();
                        HttpContext.Cache[cacheName] = _multiThreadTokenSource;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex);
                        throw;
                    }
                }

                return _multiThreadTokenSource;
            }
            set
            {
                _multiThreadTokenSource = value;
            }
        }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// The single thread cache name in the master Cache.
        /// </summary>
        /// <param name="request">The request which holds the clientId for the cache name.</param>
        /// <returns>The name of the cache.</returns>
        public static string SingleThreadCacheName(HttpRequestBase request)
        {
            // ie singleThreadTokenSource_f1fd7650-fecf-4cee-952c-f4569698317c
            string clientId = HttpContextRequestService.ReadParameter(request, "clientId");

            return $"singleThreadTokenSource_{clientId}";
        }

        /// <summary>
        /// The multithread cache name in the master Cache.
        /// </summary>
        /// <param name="request">The request which holds the clientId for the cache name.</param>
        /// <returns>The name of the cache.</returns>
        public static string MultiThreadCacheName(HttpRequestBase request)
        {
            // ie multiThreadTokenSource_f1fd7650-fecf-4cee-952c-f4569698317c
            string clientId = HttpContextRequestService.ReadParameter(request, "clientId");

            return $"multiThreadTokenSource_{clientId}";
        }
        #endregion

        /// <summary>
        /// Performs an operation over a single thread.  Used to compare
        /// single thread processing vs multithread processing.
        /// </summary>
        /// <returns>Success if passed.</returns>
        [HttpPost]
        public JsonResult SingleThread()
        {
            try
            {
                // Opening new Task with a Cancelation token
                Task task = Task.Run(() => {
                    _service.SingleThread(SingleThreadTokenSource.Token);
                }, SingleThreadTokenSource.Token);

                // Don't finish this method until the Task is done
                // Otherwise UI updates as 100% complete.
                task.Wait();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HttpContext.Response.TrySkipIisCustomErrors = true;
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message));
            }

            return Json(_ajaxSuccess);
        }

        /// <summary>
        /// Performs an operation over multiple threads.  Used to compare
        /// single thread processing vs multithread processing.
        /// </summary>
        /// <returns>Success if passed.</returns>
        [HttpPost]
        public JsonResult MultiThread()
        {
            try
            {
                // Opening new Task with a Cancelation token
                Task task = Task.Run(() => {
                    _service.MultiThread(MultiThreadTokenSource.Token);
                }, MultiThreadTokenSource.Token);

                // Don't finish this method until the Task is done
                // Otherwise UI updates as 100% complete.
                task.Wait();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HttpContext.Response.TrySkipIisCustomErrors = true;
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message));
            }

            return Json(_ajaxSuccess);
        }

        /// <summary>
        /// Cancels the single thread processing request.
        /// </summary>
        [HttpPost]
        public JsonResult CancelSingleThread()
        {
            try
            {
                SingleThreadTokenSource.Cancel();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HttpContext.Response.TrySkipIisCustomErrors = true;
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message));
            }
            finally
            {
                _singleThreadTokenSource?.Dispose();
            }

            return Json(_ajaxSuccess);
        }

        /// <summary>
        /// Cancels the multithread processing request.
        /// </summary>
        [HttpPost]
        public JsonResult CancelMultiThread()
        {
            try
            {
                MultiThreadTokenSource.Cancel();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HttpContext.Response.TrySkipIisCustomErrors = true;
                return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message));
            }
            finally
            {
                _multiThreadTokenSource?.Dispose();
            }

            return Json(_ajaxSuccess);
        }
    }
}