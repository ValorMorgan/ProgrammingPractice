using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ProgrammingPractice.Configuration;

namespace ProgrammingPractice.Error.Helper
{
    /// <summary>
    /// Handles all Error logging to the Event Viewer.
    /// </summary>
    /// <remarks>
    /// The following settings should be added and set in the application's app.config or web.config:
    /// - ExceptionDomainStackTrace: The name of the local domain stack trace in the Exception.Data field.
    /// - ExceptionLogSource: THe source for the Event Viewer log.
    /// - ExceptionLogId: The default Id for the log.
    /// </remarks>
    public static class ErrorService
    {
        /// <summary>
        /// Returns the custom message block for the exception with the local
        /// stack trace that excludes any System or other library calls.
        /// </summary>
        /// <param name="ex">The given Exception.</param>
        /// <param name="domain">The domain that raised the error (typically found with "typeof").</param>
        /// <param name="memberName">The method that is calling this function. Uses CompilerServices to get this without input.</param>
        /// <returns>The DomainStackTrace message block as a string.</returns>
        public static string LocalDomainStackTrace(Exception ex, System.Type domain,
            [CallerMemberName] string memberName = "")
        {
            int sourceLineNumber = GetSourceLineNumber(ex);

            // Setup stack trace so that the latest call is on top
            string currentMessage = $"at {domain}.{memberName} ({sourceLineNumber})";
            string previousMessage = "";
            object messageRecord = ex.Data[ApplicationSettings.AsString("ExceptionDomainStackTrace")];

            // A check is performed to confirm we already started the stack trace
            if (messageRecord != null)
                previousMessage = messageRecord.ToString();

            return $"{previousMessage}{Environment.NewLine}{currentMessage}".Trim();
        }

        /// <summary>
        /// Log the exception to the Event Viewer.
        /// </summary>
        /// <param name="ex">The exception to be logged.</param>
        public static void LogException(Exception ex)
        {
            // Log Details
            string logSource = ApplicationSettings.AsString("ExceptionLogSource");
            const EventLogEntryType logEntryType = EventLogEntryType.Error;
            int logId = ApplicationSettings.AsInt("ExceptionLogId");

            // Header
            string logHeader = $"[{DateTime.Now}] {ex.Message}";

            // Inner Exception
            string logInnerException = $"Inner Exception: {ex.InnerException?.ToString() ?? "N/A"}";

            // Body
            object domainStackTrace = ex.Data[ApplicationSettings.AsString("ExceptionDomainStackTrace")];
            string logExceptionType = $"Exception Type: {ex.GetType()}";
            string logStackTrace = $"System Stack Trace:{Environment.NewLine}{ex.StackTrace}";
            string logMessage;
            if (domainStackTrace != null) // Check if we created a domain stack trace
            {
                string logLocalDomainStackTrace = $"Local Domain Stack Trace:{Environment.NewLine}{domainStackTrace}";
                logMessage = string.Format("{1}{0}{0}{2}{0}{0}{3}{0}{0}{4}{0}{0}{5}",
                    Environment.NewLine,
                    logHeader,
                    logExceptionType,
                    logInnerException,
                    logLocalDomainStackTrace,
                    logStackTrace
                );
            }
            else
            {
                logMessage = string.Format("{1}{0}{0}{2}{0}{0}{3}{0}{0}{4}",
                    Environment.NewLine,
                    logHeader,
                    logExceptionType,
                    logInnerException,
                    logStackTrace
                );
            }

            try
            {
                EventLog.WriteEntry(logSource, logMessage, logEntryType, logId);
            }
            catch
            {
                // TODO: Handle Error logging errors in some manner

                // Throw the original exception for UI to display
                throw ex;
            }
        }

        /// <summary>
        /// Gets the line which raised/threw the exception.
        /// </summary>
        /// <param name="ex">The exception that was raised/thrown.</param>
        /// <returns>The line number of where the exception was raised/thrown.</returns>
        public static int GetSourceLineNumber(Exception ex)
        {
            int lineNumber = 0;
            const string lineSearch = ":line ";
            int index = ex.StackTrace.LastIndexOf(lineSearch, StringComparison.Ordinal);
            if (index != -1)
            {
                string lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                { /* Assigned in "out" */ }
            }
            return lineNumber;
        }
    }
}