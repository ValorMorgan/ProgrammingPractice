using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProgrammingPractice.Tests.Services
{
    /// <summary>
    /// Holds many methods for comparing and testing performance values.
    /// </summary>
    public static class PerformanceService
    {
        #region LogPerformanceToConsole
        /// <summary>
        /// Logs the performance results to the Console.
        /// </summary>
        /// <param name="message">The message to log to the Console.</param>
        public static void LogPerformanceToConsole(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Logs the performance results to the Console.
        /// </summary>
        /// <param name="expectedTime">The expected time of the operation.</param>
        /// <param name="resultTime">The result time of the operation.</param>
        public static void LogPerformanceToConsole(double expectedTime, double resultTime)
        {
            string message = string.Format(
                "Performance results:{0}Expected Time: {1}{0}Result Time: {2}",
                Environment.NewLine,
                expectedTime,
                resultTime
            );
            LogPerformanceToConsole(message);
        }

        /// <summary>
        /// Logs the performance results to the Console.
        /// </summary>
        /// <param name="expectedTime">The expected time of the operation.</param>
        /// <param name="resultTime">The result time of the operation.</param>
        /// <param name="delta">The delta value for the performance check.</param>
        public static void LogPerformanceToConsole(double expectedTime, double resultTime, double delta)
        {
            string message = string.Format(
                "Performance results:{0}Expected Time: {1}{0}Result Time: {2}{0}Delta: {3}",
                Environment.NewLine,
                expectedTime,
                resultTime,
                delta
            );
            LogPerformanceToConsole(message);
        }
        #endregion

        #region CheckMatchesExpectedTime
        /// <summary>
        /// Checks if the resulting time matches the expected time.
        /// </summary>
        /// <param name="expectedTime">The expected time.</param>
        /// <param name="resultTime">The resulting time.</param>
        public static void CheckMatchesExpectedTime(double expectedTime, double resultTime)
        {
            string assertFailMessage = string.Format(
                "Performance does not align with expected results.{0}Result: {1}{0}Expected: {2}",
                Environment.NewLine,
                resultTime,
                expectedTime
            );

            LogPerformanceToConsole(expectedTime, resultTime);

            // Perform assertion
            Assert.AreEqual(expectedTime, resultTime, assertFailMessage);
        }

        /// <summary>
        /// Checks if the resulting time matches the expected time within the given delta space.
        /// </summary>
        /// <param name="expectedTime">The expected time.</param>
        /// <param name="resultTime">The resulting time.</param>
        /// <param name="delta">The delta value for the performance test.</param>
        public static void CheckMatchesExpectedTime(double expectedTime, double resultTime, double delta)
        {
            string assertFailMessage = string.Format(
                "Performance does not align with expected results.{0}Result: {1}{0}Expected: {2}{0}Delta: {3}",
                Environment.NewLine,
                resultTime,
                expectedTime,
                delta
            );

            LogPerformanceToConsole(expectedTime, resultTime, delta);

            // Perform assertion
            Assert.AreEqual(expectedTime, resultTime, delta, assertFailMessage);
        }
        #endregion

        /// <summary>
        /// Checks if the resulting time is faster than the expected time.
        /// </summary>
        /// <param name="expectedTime">The expected time.</param>
        /// <param name="resultTime">The resulting time.</param>
        public static void CheckFasterThanExpectedTime(double expectedTime, double resultTime)
        {
            string assertFailMessage = string.Format(
                "Performance does not exceed the expected results.{0}Result: {1}{0}Expected: {2}",
                Environment.NewLine,
                resultTime,
                expectedTime
            );

            LogPerformanceToConsole(expectedTime, resultTime);

            if (resultTime > expectedTime)
                throw new AssertFailedException(assertFailMessage);
        }
    }
}