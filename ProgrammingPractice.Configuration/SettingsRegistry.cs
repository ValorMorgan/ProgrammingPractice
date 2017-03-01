using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingPractice.Configuration
{
    public static class SettingsRegistry
    {
        #region ERROR
        public static string LogSource => ApplicationSettings.AsString("LogSource");
        public static uint LogMessageId => ApplicationSettings.AsUInt("LogMessageId");
        public static uint LogExceptionId => ApplicationSettings.AsUInt("LogExceptionId");
        public static bool LogVerbose => ApplicationSettings.AsBool("LogVerbose");
        #endregion

        public static int MultiThreadWorkload => ApplicationSettings.AsInt("MultiThreadWorkload");
    }
}
