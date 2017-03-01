using System;
using System.Threading;
using System.Threading.Tasks;
using ProgrammingPractice.Interfaces;
using ProgrammingPractice.Configuration;

namespace ProgrammingPractice.Model
{
    public class MultiThreadFacade : IMultiThreadFacade
    {
        #region METHODS
        /// <summary>
        /// Test on processing the workload in a single thread.
        /// </summary>
        public void SingleThread(CancellationToken token)
        {
            int i = 0;

            while (i < SettingsRegistry.MultiThreadWorkload && !token.IsCancellationRequested)
            {
                i++;
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Test on processing the workload in multithread.
        /// </summary>
        public void MultiThread(CancellationToken token)
        {
            Parallel.For(0, SettingsRegistry.MultiThreadWorkload, i =>
            {
                if (!token.IsCancellationRequested)
                    Thread.Sleep(1000);
            });

            Task.WaitAll();
        }
        #endregion
    }
}
