using System;

namespace ProgrammingPractice.Error.Helper
{
    /// <summary>
    /// Helper for formating the provided exception.
    /// </summary>
    public static class ExceptionFormatHelper
    {
        #region PROPERTIES
        /// <summary>
        /// A simple divider for splitting up sections in the formated exception.
        /// </summary>
        public static string Divider
        {
            get
            {
                const int numberOfCharacters = 60;
                return new string('=', numberOfCharacters);
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Formats and returns the exception as an easily readable string.
        /// </summary>
        /// <param name="ex">The exception to format.</param>
        /// <returns></returns>
        public static string GetFormatedException(Exception ex)
        {
            if (ex == null)
                return "Unknown exception occurred or no exception was passed to the Logger.";

            string formatedException = null;

            string exceptionType = ex.GetType()?.ToString()?.Trim();
            string exceptionMessage = ex.Message?.Trim();
            string stackTrace = ex.StackTrace?.Trim();
            formatedException = string.Format("{1}{0}Type: {2}{0}Message: {3}{0}Logged On: {4}{0}{1}{0}Stack Trace:{0}{5}{0}{1}",
                Environment.NewLine,
                Divider,
                exceptionType,
                exceptionMessage,
                DateTime.Now,
                stackTrace
            );
            formatedException = formatedException.Trim();

            if (ex.InnerException != null)
                formatedException += string.Format("{0}{0}Inner Exception:{0}{1}",
                    Environment.NewLine,
                    GetFormatedException(ex.InnerException)
                );

            return formatedException;
        }

        /// <summary>
        /// Returns the exception messages in a single line.
        /// </summary>
        /// <param name="ex">The exception to parse.</param>
        /// <returns></returns>
        public static string GetExceptionMessages(Exception ex)
        {
            if (ex == null)
                return "Unknown exception occurred or no exception was passed to the Logger.";

            string message = (ex.Message.EndsWith(".")) ? ex.Message : $"{ex.Message}.";

            if (ex.InnerException != null)
                return $"{message} {GetExceptionMessages(ex.InnerException)}";
            else
                return ex.Message;
        }
        #endregion
    }
}