using System;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using ProgrammingPractice.Error;

namespace ProgrammingPractice.Model
{
    public static class MultiThreadFacade
    {
        #region VARIABLES
        /// <summary>
        /// The cumulative workload for the tasks.
        /// </summary>
        private static int workload { get { return 30; } }
        #endregion

        #region METHODS
        /// <summary>
        /// Test on processing the workload in a single thread.
        /// </summary>
        public static void SingleThread(CancellationToken token)
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
                ex.Data[ConfigurationManager.AppSettings["ExceptionDomainStackTrace"]] = ErrorService.LocalDomainStackTrace(ex, typeof(MultiThreadFacade));
                throw ex;
            }
        }

        /// <summary>
        /// Test on processing the workload in multithread.
        /// </summary>
        public static void MultiThread(CancellationToken token)
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
                ex.Data[ConfigurationManager.AppSettings["ExceptionDomainStackTrace"]] = ErrorService.LocalDomainStackTrace(ex, typeof(MultiThreadFacade));
                throw ex;
            }
        }
        #endregion
    }
}
