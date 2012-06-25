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
        private DateTime lastKnownWrite;
        
        public MainForm()
        {
            InitializeComponent();

            autoSaveThread = new Thread(AutoSave);
            autoSaveThread.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            splitRatio = (float)questLog.Width / Width;

            questLog.QuestLog = FileWriter.ReadFromFile();
            lastKnownWrite = FileWriter.LastFileChanged();
            showCompleted.Checked = questLog.QuestLog.ShowCompletedQuests;

            questLog.RenderLog(showCompleted.Checked);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            delete.Left = Width - 130;

            var newWidth = (int)(Width * splitRatio);
            newWidth = Width - newWidth < 176 ? Width - 176 : newWidth;
            newWidth = newWidth < 150 ? 150 : newWidth;
            questLog.Width = newWidth;
        }

        private void splitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitRatio = (float)questLog.Width / Width;
        }

        private void SetSelectionPlurality()
        {
            var currentTabControl = questLog.CurrentTabControl;

            if (currentTabControl.AnyChecked)
            {
                complete.Text = currentTabControl.AllCheckedComplete ? Resources.UncompleteChecked : Resources.CompleteChecked;

                delete.Text = Resources.DeleteChecked;
            }
            else
            {
                if (currentTabControl.LastSelectedQuest == null || !currentTabControl.LastSelectedQuest.Completed)
                    complete.Text = Resources.Complete;
                else
                    complete.Text = Resources.Uncomplete;

                delete.Text = Resources.Delete;
            }
        }

        private void complete_Click(object sender, EventArgs e)
        {
            questLog.CompleteQuests();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            questLog.DeleteQuests();
        }

        private void showCompleted_CheckedChanged(object sender, EventArgs e)
        {
            questLog.QuestLog.ShowCompletedQuests = showCompleted.Checked;
            questLog.RenderLog(showCompleted.Checked);
            questLog.SelectLastSelected();
        }

        private void AutoSave()
        {
            while (true)
            {
                Thread.Sleep(1000);

                if (questLog != null && questLog.QuestLog != null)
                {
                    if (questLog.QuestLog.Edited)
                    {
                        lock (questLog.QuestLog)
                        {
                            FileWriter.WriteToFile(questLog.QuestLog);
                            lastKnownWrite = FileWriter.LastFileChanged();
                            questLog.QuestLog.Edited = false;
                        }
                    }

                    if (lastKnownWrite < FileWriter.LastFileChanged())
                    {
                        lock (questLog.QuestLog)
                        {
                            questLog.QuestLog = FileWriter.ReadFromFile();
                            lastKnownWrite = FileWriter.LastFileChanged();
                        }
                    }
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            autoSaveThread.Abort();

            lock (questLog.QuestLog)
            {
                FileWriter.WriteToFile(questLog.QuestLog);
            }
        }

        private void questDescription_TextChanged(object sender, EventArgs e)
        {
            var currentTabControl = questLog.CurrentTabControl;

            if (questDescription.Focused)
            {
                if (currentTabControl.LastSelectedQuest != null)
                {
                    currentTabControl.LastSelectedQuest.Description = questDescription.Text;
                    questLog.QuestLog.Edited = true;
                }
            }
        }

        private void questDescription_Enter(object sender, EventArgs e)
        {
            var currentTabControl = questLog.CurrentTabControl;

            if (currentTabControl.LastSelectedQuestControl != null)
                currentTabControl.LastSelectedQuestControl.SetHighlightedBackcolor();
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

            questLog.QuestLog = FileWriter.Import(filename);
            showCompleted.Checked = questLog.QuestLog.ShowCompletedQuests;

            questLog.RenderLog(showCompleted.Checked);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var saveFileDialog = new SaveFileDialog
                                     {
                                         FileName = "QuestTracker." + now.Year.ToString("0000") + "-" + now.Month.ToString("00") + "-" + now.Day.ToString("00") + "-" + now.Hour.ToString("00") + now.Minute.ToString("00") + now.Second.ToString("00") + ".xml",
                                         Filter = Resources.XmlFiles,
                                         FilterIndex = 0,
                                         InitialDirectory = Settings.GetPath(),
                                         RestoreDirectory = true,
                                         Title = Resources.ExportFileTitle
                                     };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            lock (questLog.QuestLog)
            {
                FileWriter.Export(questLog.QuestLog, saveFileDialog.FileName);
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
            var currentTabControl = questLog.CurrentTabControl;
            if (currentTabControl.LastSelectedQuest != null)
            {
                var quest = currentTabControl.LastSelectedQuest;
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

        private void questLog_QuestSelectionChanged(object sender, EventArgs e)
        {
            ChangeQuestSelection();
        }

        private void questLog_SelectionPluralityChanged(object sender, EventArgs e)
        {
            SetSelectionPlurality();
        }
    }
}
