using ProgrammingPractice.Configuration;
using System;
using System.Diagnostics;

namespace ProgrammingPractice.Error
{
    /// <summary>
    /// Provides means for logging messages and exceptions.
    /// </summary>
    /// <remarks>Configuration settings required: "LogSource", "LogMessageId", "LogExceptionId", "LogVerbose"</remarks>
    public static class Logger
    {
        #region METHODS
        /// <summary>
        /// Logs the provided message to the Event Viewer if Log Verbose in the settings is set to true.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void LogMessage(string message)
        {
            Debug.WriteLine($"Logged Message: {message}");

            if (!SettingsRegistry.LogVerbose)
                return;

            try
            {
                EventLog.WriteEntry(SettingsRegistry.LogSource, message, EventLogEntryType.Information, (int)SettingsRegistry.LogMessageId);
            }
            catch
            {
                // NOTE: Digest errors in LogMessage as LogMessage is not crucial for operations to continue
                return;
            }
        }

        /// <summary>
        /// Logs the exception as an Information object to the Event Viewer if Log Verbose is set to true.
        /// </summary>
        /// <param name="ex">The exception to log as an information object.</param>
        public static void LogExceptionAsMessage(Exception ex)
        {
            LogMessage(Helper.ExceptionFormatHelper.GetFormatedException(ex));
        }

        /// <summary>
        /// Logs the exception as an Information object to the Event Viewer if Log Verbose is set to true.
        /// Excludes the Stack Trace information from the message.
        /// </summary>
        /// <param name="ex">The exception to log as an information object.</param>
        public static void LogExceptionAsMessageWithoutStackTrace(Exception ex)
        {
            LogMessage(Helper.ExceptionFormatHelper.GetExceptionMessages(ex));
        }

        /// <summary>
        /// Logs the provided exception to the Event Viewer.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        public static void LogException(Exception ex)
        {
            string logMessage = Helper.ExceptionFormatHelper.GetFormatedException(ex);

            try
            {
                EventLog.WriteEntry(SettingsRegistry.LogSource, logMessage, EventLogEntryType.Error, (int)SettingsRegistry.LogExceptionId);
            }
            catch
            {
                // Failed to log so throw original exception for default .NET handling
                throw ex;
            }
        }
        #endregion
    }
}