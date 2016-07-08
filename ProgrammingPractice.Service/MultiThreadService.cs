using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammingPractice.Model;

namespace ProgrammingPractice.Service
{
    public static class MultiThreadService
    {
        #region METHODS
        public static void SingleThread()
        {
            try
            {
                MultiThreadFacade.SingleThread();
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
                MultiThreadFacade.MultiThread();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
