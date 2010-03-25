using System;
using System.Diagnostics;

namespace QuestTracker
{
    static class ErrorLog
    {
        public static void LogError(Exception ex)
        {
            const string applicationName = "QuestTracker";

            if (!EventLog.SourceExists(applicationName))
                EventLog.CreateEventSource(applicationName, "Application");

            EventLog.WriteEntry(applicationName, ex.ToString(), EventLogEntryType.Error, 0);
        }
    }
}
