using System;
using System.Windows.Forms;

namespace QuestTracker
{
    static class QuestTrackerApp
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                throw;
            }

        }
    }
}
