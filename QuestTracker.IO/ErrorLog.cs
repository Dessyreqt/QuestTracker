using System;
using System.Diagnostics;
using System.IO;

namespace QuestTracker.IO
{
    public static class ErrorLog
    {
        public static void LogError(Exception ex)
        {
            
            if (Settings.PortableMode)
                LogErrorFile(ex);
            else
                LogErrorEvent(ex);
        }

        private static void LogErrorFile(Exception ex)
        {
            using (var stream = new FileStream("ErrorLog.txt", FileMode.Append))
            {
                var writer = new StreamWriter(stream);
                writer.Write("<");
                writer.Write(DateTime.Now);
                writer.WriteLine(">");
                writer.WriteLine(ex.ToString());
                writer.WriteLine();
            }

        }

        private static void LogErrorEvent(Exception ex)
        {
            const string applicationName = "QuestTracker";

            if (!EventLog.SourceExists(applicationName))
                EventLog.CreateEventSource(applicationName, "Application");

            EventLog.WriteEntry(applicationName, ex.ToString(), EventLogEntryType.Error, 0);
        }
    }
}
