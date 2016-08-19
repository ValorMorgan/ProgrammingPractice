using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingPractice.Interfaces;
using ProgrammingPractice.Configuration;
using ProgrammingPractice.Tests.Services;

namespace ProgrammingPractice.Model.Tests
{
    [TestClass]
    public class MultiThreadFacadeTests
    {
        [TestMethod]
        public void SingleThread_TimeToComplete_AsFastAsExpected()
        {
            IMultiThreadFacade multiThreadFacade = new MultiThreadFacade();
            CancellationToken token = new CancellationToken();

            // Time the actual process
            Stopwatch watch = Stopwatch.StartNew();
            multiThreadFacade.SingleThread(token);
            watch.Stop();

            // Setup details
            double expected = multiThreadFacade.workload * TimeSpan.FromSeconds(1).TotalSeconds;
            double result = watch.Elapsed.TotalSeconds;
            double delta = TimeSpan.FromSeconds(0.5).TotalSeconds;

            // Check if passed
            PerformanceService.CheckMatchesExpectedTime(expected, result, delta);
        }

        [TestMethod]
        public void MultiThread_TimeToComplete_FasterThanExpected()
        {
            IMultiThreadFacade multiThreadFacade = new MultiThreadFacade();
            CancellationToken token = new CancellationToken();
            ParallelOptions options = new ParallelOptions();

            // Time the actual process
            Stopwatch watch = Stopwatch.StartNew();
            multiThreadFacade.MultiThread(token);
            watch.Stop();

            // Setup details
            double expected = multiThreadFacade.workload * TimeSpan.FromSeconds(1).TotalSeconds;
            double result = watch.Elapsed.TotalSeconds;

            // Check if passed
            PerformanceService.CheckFasterThanExpectedTime(expected, result);

        }
    }
}
