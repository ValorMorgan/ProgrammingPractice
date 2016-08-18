using System;
using System.Threading;
using System.Configuration;
using ProgrammingPractice.Error;
using ProgrammingPractice.Model;

namespace ProgrammingPractice.Service
{
    /// <summary>
    /// Access point from UI to perform operations for
    /// MultiThreading.
    /// </summary>
    public static class MultiThreadService
    {
        #region METHODS
        /// <summary>
        /// Test on processing the workload in a single thread.
        /// </summary>
        public static void SingleThread(CancellationToken token)
        {
            try
            {
                MultiThreadFacade.SingleThread(token);
            }
            catch (Exception ex)
            {
                ex.Data[ConfigurationManager.AppSettings["ExceptionDomainStackTrace"]] = ErrorService.LocalDomainStackTrace(ex, typeof(MultiThreadService));
                ErrorService.LogException(ex);
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
                MultiThreadFacade.MultiThread(token);
            }
            catch (Exception ex)
            {
                ex.Data[ConfigurationManager.AppSettings["ExceptionDomainStackTrace"]] = ErrorService.LocalDomainStackTrace(ex, typeof(MultiThreadService));
                ErrorService.LogException(ex);
                throw ex;
            }
        }
        #endregion
    }
}
