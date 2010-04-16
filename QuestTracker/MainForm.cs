using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace QuestTracker
{
    public partial class MainForm : Form
    {
        public QuestLog questLog;
        public QuestGroup lastSelectedQuestGroup;
        public QuestGroupControl lastSelectedQuestGroupControl;
        public Quest lastSelectedQuest;
        public QuestControl lastSelectedQuestControl;

        private float splitRatio;
        private readonly Thread autoSaveThread;
        public readonly List<QuestControl> questControls;
        private readonly List<QuestGroupControl> questGroupControls;
        private bool anyChecked;
        private bool allCheckedComplete = true;


        public MainForm()
        {
            InitializeComponent();

            questControls = new List<QuestControl>();
            questGroupControls = new List<QuestGroupControl>();

            autoSaveThread = new Thread(AutoSave);
            autoSaveThread.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            splitRatio = (float)quests.Width / Width;

            questLog = FileWriter.ReadFromFile();
            showCompleted.Checked = questLog.ShowCompletedQuests;

            RenderLog();
        }

        public void RenderLog()
        {
            int questScrollOffset = quests.VerticalScroll.Value;
            
            quests.Controls.Clear();
            questControls.Clear();
            questGroupControls.Clear();

            foreach(var questGroup in questLog.Groups)
            {
                var questGroupControl = new QuestGroupControl {Dock = DockStyle.Top, QuestGroup = questGroup};

                foreach (Control control in questGroupControl.Controls)
                {
                    if (control.GetType() != typeof (QuestControl)) 
                        continue;

                    var questControl = (QuestControl)control;  

                    questControls.Add(questControl);
                    if (questControl.Quest == lastSelectedQuest)
                        lastSelectedQuestControl = questControl;

                    if (questControl.Quest.Completed && !showCompleted.Checked)
                    {
                        questControl.Visible = false;
                        questGroupControl.Height -= 24;
                    }

                }

                quests.Controls.Add(questGroupControl);
                questGroupControls.Add(questGroupControl);

                if (questGroupControl.QuestGroup == lastSelectedQuestGroup)
                    lastSelectedQuestGroupControl = questGroupControl;

                if (questLog.Groups.Count == 1)
                {
                    lastSelectedQuestGroupControl = questGroupControl;
                }

                questGroupControl.BringToFront();
            }

            var addGroup = new AddGroup {Dock = DockStyle.Top};
            quests.Controls.Add(addGroup);
            addGroup.BringToFront();
            
            SetSelectionPlurality();

            quests.VerticalScroll.Value = questScrollOffset;
            quests.Refresh();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            delete.Left = Width - 130;

            var newWidth = (int)(Width * splitRatio);
            newWidth = Width - newWidth < 176 ? Width - 176 : newWidth;
            newWidth = newWidth < 150 ? 150 : newWidth;
            quests.Width = newWidth;
        }

        private void splitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitRatio = (float)quests.Width / Width;
        }

        public void SetSelectionPlurality()
        {
            anyChecked = false;
            allCheckedComplete = true;

            foreach (var questControl in questControls)
            {
                if (questControl.selected.Checked)
                {
                    anyChecked = true;
                    if (!questControl.Quest.Completed)
                        allCheckedComplete = false;
                }
            }

            foreach (var questGroupControl in questGroupControls)
            {
                if (questGroupControl.selected.Checked)
                {
                    anyChecked = true;
                }
            }

            if (anyChecked)
            {
                if (allCheckedComplete)
                    complete.Text = "Uncomplete Checked";
                else
                    complete.Text = "Complete Checked";

                delete.Text = "Delete Checked";
            }
            else
            {
                if (lastSelectedQuest == null || !lastSelectedQuest.Completed)
                    complete.Text = "Complete";
                else
                    complete.Text = "Uncomplete";

                delete.Text = "Delete";
            }
        }

        private void complete_Click(object sender, EventArgs e)
        {
            if (anyChecked)
            {
                if (allCheckedComplete)
                {
                    foreach (var questControl in questControls)
                    {
                        if (!questControl.selected.Checked) continue;

                        questControl.Quest.Completed = false;
                        questControl.Quest.CompleteDate = DateTime.MinValue;
                        questControl.selected.Checked = false;
                    }

                    RenderLog();
                    SelectLastSelected();
                }
                else
                {
                    foreach (var questControl in questControls)
                    {
                        if (!questControl.selected.Checked) continue;

                        var quest = questControl.Quest;
                        quest.Completed = true;

                        if (quest.CompleteDate == DateTime.MinValue)
                            quest.CompleteDate = DateTime.Now;

                        questControl.selected.Checked = false;
                    }

                    RenderLog();
                    SelectLastSelected();
                }
            }
            else
            {
                if (lastSelectedQuest != null)
                {
                    if (lastSelectedQuest.Completed)
                    {
                        lastSelectedQuest.Completed = false;
                        lastSelectedQuest.CompleteDate = DateTime.MinValue;
                    }
                    else
                    {
                        lastSelectedQuest.Completed = true;

                        if (lastSelectedQuest.CompleteDate == DateTime.MinValue)
                            lastSelectedQuest.CompleteDate = DateTime.Now;
                    }

                    RenderLog();
                    SelectLastSelected();
                }
            }
        }

        private void SelectLastSelected()
        {
            if (lastSelectedQuestControl != null)
                lastSelectedQuestControl.Focus();
            else if (lastSelectedQuestGroupControl != null) 
                lastSelectedQuestGroupControl.Focus();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (anyChecked)
            {
                FileWriter.Export(questLog);
                
                foreach (var questGroup in questGroupControls)
                {
                    foreach (var questControl in questControls)
                    {
                        if (questControl.selected.Checked)
                        {
                            questGroup.QuestGroup.Quests.Remove(questControl.Quest);
                        }
                    }

                    if (questGroup.selected.Checked)
                        questLog.Groups.Remove(questGroup.QuestGroup);
                }

                RenderLog();
            }
            else
            {
                if (lastSelectedQuest != null)
                {
                    FileWriter.Export(questLog);

                    lastSelectedQuestGroup.Quests.Remove(lastSelectedQuest);

                    RenderLog();
                }
            }
        }

        private void showCompleted_CheckedChanged(object sender, EventArgs e)
        {
            questLog.ShowCompletedQuests = showCompleted.Checked;
            RenderLog();
            SelectLastSelected();
        }

        private void AutoSave()
        {
            while (true)
            {
                Thread.Sleep(15000);

                FileWriter.WriteToFile(questLog);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            autoSaveThread.Abort();
            FileWriter.WriteToFile(questLog);
        }

        private void questDescription_TextChanged(object sender, EventArgs e)
        {
            if (questDescription.Focused)
                if (lastSelectedQuest != null)
                    lastSelectedQuest.Description = questDescription.Text;
        }

        private void questDescription_Enter(object sender, EventArgs e)
        {
            if (lastSelectedQuestControl != null) 
                lastSelectedQuestControl.SetHighlightedBackcolor();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                                     {
                                         Filter = "XML files (*.xml)|*.xml",
                                         FilterIndex = 0,
                                         InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\QuestTracker\\",
                                         RestoreDirectory = true,
                                         Title = "Select File to Import"
                                     };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            var filename = openFileDialog.FileName;

            questLog = FileWriter.Import(filename);
            showCompleted.Checked = questLog.ShowCompletedQuests;

            RenderLog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var saveFileDialog = new SaveFileDialog
                                     {
                                         FileName = "QuestTracker." + now.Month.ToString("00") + "-" + now.Day.ToString("00") + "-" + now.Year.ToString("0000") + "-" + now.Hour.ToString("00") + now.Minute.ToString("00") + now.Second.ToString("00") + ".xml",
                                         Filter = "XML files (*.xml)|*.xml",
                                         FilterIndex = 0,
                                         InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\QuestTracker\\",
                                         RestoreDirectory = true,
                                         Title = "Select file to Export"
                                     };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileWriter.Export(questLog, saveFileDialog.FileName);
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
    }
}
