using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgrammingPractice.Model
{
    public static class MultiThreadFacade
    {
        #region VARIABLES
        private static int workload { get { return 10; } }
        #endregion

        #region METHODS
        public static void SingleThread()
        {
            try
            {
                int i = 0;

                while (i < workload)
                {
                    i++;
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void MultiThread()
        {
            try
            {
                Parallel.For(0, workload, i =>
                {
                    Thread.Sleep(1000);
                });

                Task.WaitAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
