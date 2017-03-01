using System;
using System.Threading;
using ProgrammingPractice.Interfaces;
using ProgrammingPractice.Error;
using ProgrammingPractice.Model;

namespace ProgrammingPractice.Service
{
    /// <summary>
    /// Access point from UI to perform operations for
    /// MultiThreading.
    /// </summary>
    public class MultiThreadService : IMultiThreadService
    {
        #region VARIABLES
        private readonly IMultiThreadFacade _multiThreadFacade = new MultiThreadFacade();
        #endregion

        #region METHODS
        /// <summary>
        /// Test on processing the workload in a single thread.
        /// </summary>
        public void SingleThread(CancellationToken token)
        {
            try
            {
                _multiThreadFacade.SingleThread(token);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Test on processing the workload in multithread.
        /// </summary>
        public void MultiThread(CancellationToken token)
        {
            try
            {
                _multiThreadFacade.MultiThread(token);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw;
            }
        }
        #endregion
    }
}
