using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ProgrammingPractice.Interfaces;
using ProgrammingPractice.Configuration;

namespace ProgrammingPractice.Error
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
        /// <param name="sourceLineNumber">The line number at where the call came from. Uses CompilerServices to get this wihtout input.</param>
        /// <returns>The DomainStackTrace message block as a string.</returns>
        public static string LocalDomainStackTrace(Exception ex, System.Type domain,
            [CallerMemberName] string memberName = "")
        {
            IApplicationSettings applicationSettings = new ApplicationSettings();

            int sourceLineNumber = GetSourceLineNumber(ex);

            // Setup stack trace so that the latest call is on top
            string currentMessage = string.Format("at {0}.{1} ({2})", domain.ToString(), memberName, sourceLineNumber);
            string previousMessage = "";
            object messageRecord = ex.Data[applicationSettings["ExceptionDomainStackTrace"]];

            // A check is performed to confirm we already started the stack trace
            if (messageRecord != null)
                previousMessage = messageRecord.ToString();

            return string.Format("{0}\n{1}", previousMessage, currentMessage).Trim();
        }

        /// <summary>
        /// Log the exception to the Event Viewer.
        /// </summary>
        /// <param name="ex">The exception to be logged.</param>
        public static void LogException(Exception ex)
        {
            IApplicationSettings applicationSettings = new ApplicationSettings();

            // Log Details
            string logSource = applicationSettings["ExceptionLogSource"];
            EventLogEntryType logEntryType = EventLogEntryType.Error;
            int logId = int.Parse(applicationSettings["ExceptionLogId"]);

            // Header
            string logHeader = string.Format("[{0}] {1}", DateTime.Now.ToString(), ex.Message);

            // Inner Exception
            string logInnerException = string.Format("Inner Exception: {0}", (ex.InnerException != null) ? ex.InnerException.ToString() : "N/A");

            // Body
            object domainStackTrace = ex.Data[applicationSettings["ExceptionDomainStackTrace"]];
            string logExceptionType = string.Format("Exception Type: {0}", ex.GetType().ToString());
            string logStackTrace = string.Format("System Stack Trace\n{0}", ex.StackTrace);
            string logMessage;
            if (domainStackTrace != null) // Check if we created a domain stack trace
            {
                string logLocalDomainStackTrace = string.Format("Local Domain Stack Trace\n{0}", domainStackTrace);
                logMessage = string.Format("{0}\n\n{1}\n\n{2}\n\n{3}\n\n{4}", logHeader, logExceptionType, logInnerException, logLocalDomainStackTrace, logStackTrace);
            }
            else
            {
                logMessage = string.Format("{0}\n\n{1}\n\n{2}\n\n{3}", logHeader, logExceptionType, logInnerException, logStackTrace);
            }

            try
            {
                EventLog.WriteEntry(logSource, logMessage, logEntryType, logId);
            }
            catch (Exception loggingException)
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
            var lineNumber = 0;
            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            if (index != -1)
            {
                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                { /* Assigned in "out" */ }
            }
            return lineNumber;
        }
    }
}