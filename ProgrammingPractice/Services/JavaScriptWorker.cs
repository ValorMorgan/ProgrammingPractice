using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.UI;
using ProgrammingPractice.Service;

namespace ProgrammingPractice.Services
{
    public class JavaScriptWorker : IDisposable
    {
        #region VARIABLES
        private BackgroundWorker worker { get; set; }

        public Page parentPage { get; set; }

        public string javascriptFunction { get; set; }

        public NameValueCollection javascriptParameters { get; set; }
        #endregion

        #region CONSTRUCTOR
        public JavaScriptWorker(Page parentPage)
        {
            this.parentPage = parentPage;
            worker = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        public JavaScriptWorker(Page parentPage, string javascriptFunction)
            : this(parentPage)
        {
            this.javascriptFunction = javascriptFunction;
        }

        public JavaScriptWorker(Page parentPage, string javascriptFunction, NameValueCollection javascriptParameters)
            : this(parentPage, javascriptFunction)
        {
            this.javascriptParameters = javascriptParameters;
        }
        #endregion

        #region METHODS
        public void BeginWork(object work)
        {
            worker.RunWorkerAsync(work);
        }

        public void CancelWork()
        {
            worker.CancelAsync();
        }
        #endregion

        #region EVENTS
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string work = e.Argument.ToString();

            switch (work)
            {
                case "singleThread":
                    Service.MultiThreadService.SingleThread();
                    break;
                case "multiThread":
                    Service.MultiThreadService.MultiThread();
                    break;
                default:
                    throw new NotImplementedException(string.Format("JavaScriptWorker - Work unrecognized: {0}", work));
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(worker.CancellationPending)
            {
                CancelWork();
                return;
            }

            string parameters = "";
            foreach (string key in javascriptParameters.AllKeys)
            {
                if (key.ToLower().Contains("progress"))
                    javascriptParameters[key] = e.ProgressPercentage.ToString();

                if (string.IsNullOrEmpty(parameters))
                    parameters = string.Format("{0}=\'{1}\'", key, javascriptParameters[key]).Trim();
                else
                    parameters = string.Format("{0}, {1}=\'{2}\'", parameters, key, javascriptParameters[key]).Trim();
            }

            string javascriptCall = string.Format("{0}({1});", javascriptFunction, parameters);
            ScriptManager.RegisterClientScriptBlock(parentPage, parentPage.GetType(), "WorkerUpdate", javascriptCall, true);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            worker.ReportProgress(100);
        }
        #endregion

        #region DISPOSE
        public void Dispose()
        {
            worker.Dispose();
        }
        #endregion
    }
}