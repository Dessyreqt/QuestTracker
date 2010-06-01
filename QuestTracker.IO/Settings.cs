using System;
using System.Configuration;

namespace QuestTracker.IO
{
    public static class Settings
    {             
        public static bool PortableMode
        {
            get
            {
                bool retVal;
                bool.TryParse(ConfigurationManager.AppSettings["portableMode"], out retVal);
                return retVal;
            }
        }

        public static string GetPath()
        {
            if (PortableMode)
                return Environment.CurrentDirectory + "\\QuestLogs\\";
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\QuestTracker\\";
        }
    }
}
