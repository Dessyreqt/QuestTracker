using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using QuestTracker.IO;
using QuestTracker.Properties;
using Settings = QuestTracker.IO.Settings;

namespace QuestTracker
{
    public partial class MainForm : Form
    {
        private float splitRatio;
        private readonly Thread autoSaveThread;

        public MainForm()
        {
            InitializeComponent();

            autoSaveThread = new Thread(AutoSave);
            autoSaveThread.Start();

            //recurringQuestWorker.RunWorkerAsync();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            splitRatio = (float)questTabs.Width / Width;

            questLogControl.QuestLog = FileWriter.ReadFromFile();
            showCompleted.Checked = questLogControl.QuestLog.ShowCompletedQuests;

            questLogControl.RenderLog(showCompleted.Checked);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            delete.Left = Width - 130;

            var newWidth = (int)(Width * splitRatio);
            newWidth = Width - newWidth < 176 ? Width - 176 : newWidth;
            newWidth = newWidth < 150 ? 150 : newWidth;
            questTabs.Width = newWidth;
        }

        private void splitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitRatio = (float)questTabs.Width / Width;
        }

        private void SetSelectionPlurality()
        {
            if (questLogControl.AnyChecked)
            {
                complete.Text = questLogControl.AllCheckedComplete ? Resources.UncompleteChecked : Resources.CompleteChecked;

                delete.Text = Resources.DeleteChecked;
            }
            else
            {
                if (questLogControl.LastSelectedQuest == null || !questLogControl.LastSelectedQuest.Completed)
                    complete.Text = Resources.Complete;
                else
                    complete.Text = Resources.Uncomplete;

                delete.Text = Resources.Delete;
            }
        }

        private void complete_Click(object sender, EventArgs e)
        {
            questLogControl.CompleteQuests();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            questLogControl.DeleteQuests();
        }

        private void showCompleted_CheckedChanged(object sender, EventArgs e)
        {
            questLogControl.QuestLog.ShowCompletedQuests = showCompleted.Checked;
            questLogControl.RenderLog(showCompleted.Checked);
            questLogControl.SelectLastSelected();
        }

        private void AutoSave()
        {
            while (true)
            {
                Thread.Sleep(15000);

                lock(questLogControl.QuestLog)
                {
                    FileWriter.WriteToFile(questLogControl.QuestLog);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            autoSaveThread.Abort();

            lock (questLogControl.QuestLog)
            {
                FileWriter.WriteToFile(questLogControl.QuestLog);
            }
        }

        private void questDescription_TextChanged(object sender, EventArgs e)
        {
            if (questDescription.Focused)
                if (questLogControl.LastSelectedQuest != null)
                    questLogControl.LastSelectedQuest.Description = questDescription.Text;
        }

        private void questDescription_Enter(object sender, EventArgs e)
        {
            if (questLogControl.LastSelectedQuestControl != null)
                questLogControl.LastSelectedQuestControl.SetHighlightedBackcolor();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                                     {
                                         Filter = Resources.XmlFiles,
                                         FilterIndex = 0,
                                         InitialDirectory = Settings.GetPath(),
                                         RestoreDirectory = false,
                                         Title = Resources.ImportFileTitle
                                     };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var filename = openFileDialog.FileName;

            questLogControl.QuestLog = FileWriter.Import(filename);
            showCompleted.Checked = questLogControl.QuestLog.ShowCompletedQuests;

            questLogControl.RenderLog(showCompleted.Checked);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var saveFileDialog = new SaveFileDialog
                                     {
                                         FileName = "QuestTracker." + now.Month.ToString("00") + "-" + now.Day.ToString("00") + "-" + now.Year.ToString("0000") + "-" + now.Hour.ToString("00") + now.Minute.ToString("00") + now.Second.ToString("00") + ".xml",
                                         Filter = Resources.XmlFiles,
                                         FilterIndex = 0,
                                         InitialDirectory = Settings.GetPath(),
                                         RestoreDirectory = true,
                                         Title = Resources.ExportFileTitle
                                     };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            
            lock (questLogControl.QuestLog)
            {
                FileWriter.Export(questLogControl.QuestLog, saveFileDialog.FileName);
            }
        }

        private void questTrackerWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(null, "http://questtracker.codeplex.com/");
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(null, "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=FDSTUC7EAYTH2");
        }

        private void questDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                questDescription.SelectAll();
            }
        }

        private void ChangeQuestSelection()
        {
            if (questLogControl.LastSelectedQuest != null)
            {
                var quest = questLogControl.LastSelectedQuest;
                startDate.Text = Resources.DateStarted + quest.StartDate;
                completeDate.Visible = true;
                if (quest.Completed)
                {
                    completeDate.ForeColor = Color.Green;
                    completeDate.Text = Resources.DateLastCompleted + quest.CompleteDates[quest.CompleteDates.Count - 1];
                }
                else
                {
                    completeDate.ForeColor = Color.Red;
                    completeDate.Text = Resources.NotCompleted;
                }

                questDescription.Enabled = true;
                questDescription.Lines = quest.Description.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
            }
            else
            {
                startDate.Text = Resources.DateStarted;
                completeDate.Visible = false;

                questDescription.Enabled = false;
                questDescription.Text = "";
            }
        }

        private void questLogControl_QuestSelectionChanged(object sender, EventArgs e)
        {
            ChangeQuestSelection();
        }

        private void questLogControl_SelectionPluralityChanged(object sender, EventArgs e)
        {
            SetSelectionPlurality();
        }
    }
}
