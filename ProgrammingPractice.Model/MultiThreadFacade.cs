﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgrammingPractice.Model
{
    public static class MultiThreadFacade
    {
        #region METHODS
        public static void SingleThread()
        {
            try
            {
                int i = 0;

                while (i < 100)
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
                int totalJobs = 100;

                Parallel.For(0, totalJobs, i =>
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
