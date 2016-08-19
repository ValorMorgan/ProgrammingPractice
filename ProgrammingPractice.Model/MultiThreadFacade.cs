using System;
using System.Threading;
using System.Threading.Tasks;
using ProgrammingPractice.Error;
using ProgrammingPractice.Interfaces;
using ProgrammingPractice.Configuration;

namespace ProgrammingPractice.Model
{
    public class MultiThreadFacade : IMultiThreadFacade
    {
        #region VARIABLES
        IApplicationSettings _applicationSettings = new ApplicationSettings();
        #endregion

        #region PROPERTIES
        /// <summary>
        /// The cumulative workload for the tasks.
        /// </summary>
        public int workload { get { return int.Parse(_applicationSettings["WorkLoadAmount"]); } }
        #endregion

        #region METHODS
        /// <summary>
        /// Test on processing the workload in a single thread.
        /// </summary>
        public void SingleThread(CancellationToken token)
        {
            try
            {
                int i = 0;

                while (i < workload && !token.IsCancellationRequested)
                {
                    i++;
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                ex.Data[_applicationSettings["ExceptionDomainStackTrace"]] = ErrorService.LocalDomainStackTrace(ex, typeof(MultiThreadFacade));
                throw ex;
            }
        }

        /// <summary>
        /// Test on processing the workload in multithread.
        /// </summary>
        public void MultiThread(CancellationToken token)
        {
            try
            {
                Parallel.For(0, workload, i =>
                {
                    if (!token.IsCancellationRequested)
                        Thread.Sleep(1000);
                });

                Task.WaitAll();
            }
            catch (Exception ex)
            {
                ex.Data[_applicationSettings["ExceptionDomainStackTrace"]] = ErrorService.LocalDomainStackTrace(ex, typeof(MultiThreadFacade));
                throw ex;
            }
        }
        #endregion
    }
}
