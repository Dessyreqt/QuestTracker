using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace QuestTracker
{
    public partial class MainForm : Form
    {
        public QuestLog questLog;
        private QuestGroup lastSelectedQuestGroup;
        public QuestGroupControl lastSelectedQuestGroupControl;
        private Quest lastSelectedQuest;
        public QuestControl lastSelectedQuestControl;
        public readonly List<QuestControl> questControls;
        public bool dialogOpen;
        public Form dialog;
        public bool forcedCollapse;

        private float splitRatio;
        private readonly Thread autoSaveThread;
        private readonly List<QuestGroupControl> questGroupControls;
        private bool anyChecked;
        private bool allCheckedComplete = true;
        bool addGroupPresent;

        public QuestGroup LastSelectedQuestGroup
        {
            get { return lastSelectedQuestGroup; }
            set
            {
                lastSelectedQuestGroup = value;

                if (value != null)
                {
                    var lastSelectedQuery = from QuestGroupControl questGroupControl in questGroupControls
                                            where questGroupControl.QuestGroup == lastSelectedQuestGroup
                                            select questGroupControl;

                    lastSelectedQuestGroupControl = lastSelectedQuery.First();
                }
            }
        }

        public Quest LastSelectedQuest
        {
            get { return lastSelectedQuest; }
            set
            {
                lastSelectedQuest = value;

                if (value != null)
                {

                    var lastSelectedQuery = from QuestControl questControl in questControls
                                            where questControl.Quest == lastSelectedQuest
                                            select questControl;

                    lastSelectedQuestControl = lastSelectedQuery.First();
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();

            questControls = new List<QuestControl>();
            questGroupControls = new List<QuestGroupControl>();

            autoSaveThread = new Thread(AutoSave);
            autoSaveThread.Start();

            //recurringQuestWorker.RunWorkerAsync();
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
            
            //remove groups that aren't in the log
            var controlsToDelete = (from Control questGroupControl in quests.Controls.Cast<Control>()
                                   where questGroupControl.GetType() == typeof(QuestGroupControl) && !questLog.Groups.Contains(((QuestGroupControl)questGroupControl).QuestGroup)
                                   select (QuestGroupControl)questGroupControl).ToList();

            foreach (QuestGroupControl questGroupControl in controlsToDelete)
            {
                foreach (QuestControl quest in questGroupControl.questControls)
                {
                    questControls.Remove(quest);
                }

                quests.Controls.Remove(questGroupControl);
                questGroupControls.Remove(questGroupControl);
            }

            //add new groups that are in the log
            var questGroupsInControls = from Control questGroupControl in quests.Controls.Cast<Control>()
                                   where questGroupControl.GetType() == typeof(QuestGroupControl)
                                   select ((QuestGroupControl)questGroupControl).QuestGroup;

            var questGroupsToAdd = from QuestGroup questGroup in questLog.Groups
                              where !questGroupsInControls.Contains(questGroup)
                              select questGroup;

            foreach (QuestGroup questGroup in questGroupsToAdd)
            {
                var questGroupControl = new QuestGroupControl {Dock = DockStyle.Top, QuestGroup = questGroup, TabStop = false, ShowCompleted = showCompleted.Checked};

                quests.Controls.Add(questGroupControl);
                questGroupControls.Add(questGroupControl);
                questGroupControl.BringToFront();
            }

            foreach (QuestGroupControl control in questGroupControls)
            {
                if (control.GetType() == typeof(QuestGroupControl))
                {
                    var questGroupControl = control;
                    questGroupControl.ShowCompleted = showCompleted.Checked;
                    questGroupControl.RenderGroup();
                }
            }

            if (!addGroupPresent)
            {
                var addGroup = new AddGroup { Dock = DockStyle.Top, TabStop = false };
                quests.Controls.Add(addGroup);
                addGroupPresent = true;
            }

            FixZOrder();

            var addGroupQuery = from Control addGroup in quests.Controls.Cast<Control>()
                           where addGroup.GetType() == typeof(AddGroup)
                           select (AddGroup)addGroup;
            
            addGroupQuery.First().BringToFront();

            quests.VerticalScroll.Value = Math.Min(questScrollOffset, quests.VerticalScroll.Maximum);
            quests.Refresh();
        }

        private void FixZOrder()
        {
            var controlsToCheck = new List<QuestGroupControl>();

            foreach (Control control in quests.Controls)
            {
                if (control.GetType() != typeof(QuestGroupControl))
                    continue;

                controlsToCheck.Add((QuestGroupControl)control);
            }

            for (int i = 0; i < questLog.Groups.Count; i++)
            {
                foreach (QuestGroupControl control in controlsToCheck)
                {
                    if (control.QuestGroup == questLog.Groups[i])
                    {
                        control.BringToFront();
                        break;
                    }
                }
            }
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
                complete.Text = allCheckedComplete ? "Uncomplete Checked" : "Complete Checked";

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
                        questControl.Quest.CompleteDates.RemoveAt(questControl.Quest.CompleteDates.Count - 1);
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

                        if (!quest.Completed)
                        {
                            quest.CompleteDates.Add(DateTime.Now);
                            quest.Completed = true;
                            SetNextRecurDate(quest);
                        }

                        questControl.selected.Checked = false;
                    }

                    RenderLog();
                    SelectLastSelected();
                }

                foreach (QuestGroupControl questGroupControl in questGroupControls)
                {
                    questGroupControl.selected.Checked = false;
                }
            }
            else
            {
                if (lastSelectedQuest != null)
                {
                    if (lastSelectedQuest.Completed)
                    {
                        lastSelectedQuest.Completed = false;
                        lastSelectedQuest.CompleteDates.RemoveAt(lastSelectedQuest.CompleteDates.Count - 1);
                    }
                    else
                    {
                        lastSelectedQuest.Completed = true;
                        lastSelectedQuest.CompleteDates.Add(DateTime.Now);
                        SetNextRecurDate(lastSelectedQuest);
                    }

                    RenderLog();
                    SelectLastSelected();
                }
            }

            SetSelectionPlurality();
        }

        public void SelectLastSelected()
        {
            if (lastSelectedQuestControl != null)
                lastSelectedQuestControl.Focus();
            else if (lastSelectedQuestGroupControl != null) 
                lastSelectedQuestGroupControl.Focus();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DeleteQuests();
        }

        public void DeleteQuests()
        {
            if (MessageBox.Show("Are you sure you wish to delete the selected quests?\n\nA backup of the current file will be made if you choose to delete.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            if (anyChecked)
            {
                FileWriter.Export(questLog);
                
                foreach (var questGroup in questGroupControls)
                {
                    foreach (var questControl in questGroup.questControls)
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

            SetSelectionPlurality();
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

        private void CheckRecurringQuests()
        {
            while (true)
            {
                if (questLog != null)
                {
                    bool refreshLog = false;

                    foreach (var group in questLog.Groups)
                    {
                        foreach (var quest in group.Quests)
                        {
                            if (!quest.Recurring) continue;
                            if (!quest.Completed) continue;

                            if (quest.Schedule.Unit == RecurrenceUnit.Minutes && quest.Schedule.StartDate.AddMinutes(quest.Schedule.Frequency) < DateTime.Now)
                            {
                                RecurQuest(quest);
                                refreshLog = true;
                            }
                            if (quest.Schedule.Unit == RecurrenceUnit.Hours && quest.Schedule.StartDate.AddHours(quest.Schedule.Frequency) < DateTime.Now)
                            {
                                RecurQuest(quest);
                                refreshLog = true;
                            }
                            if (quest.Schedule.Unit == RecurrenceUnit.Days && quest.Schedule.StartDate.AddDays(quest.Schedule.Frequency) < DateTime.Now)
                            {
                                RecurQuest(quest);
                                refreshLog = true;
                            }
                        }
                    }

                    if (refreshLog)
                        recurringQuestWorker.ReportProgress(0);
                }

                Thread.Sleep(5000);
            }
        }

        private void RecurQuest(Quest quest)
        {
            quest.Completed = false;
            SetNextRecurDate(quest);
        }

        private static void SetNextRecurDate(Quest quest)
        {
            if (!quest.Recurring) return;

            TimeSpan timeElapsed = DateTime.Now - quest.Schedule.StartDate;

            switch(quest.Schedule.Unit)
            {
                case RecurrenceUnit.Minutes:
                    quest.Schedule.StartDate = quest.Schedule.StartDate.AddMinutes((int)timeElapsed.TotalMinutes - ((int)timeElapsed.TotalMinutes % quest.Schedule.Frequency));
                    break;
                case RecurrenceUnit.Hours:
                    quest.Schedule.StartDate = quest.Schedule.StartDate.AddHours((int)timeElapsed.TotalHours - ((int)timeElapsed.TotalHours % quest.Schedule.Frequency));
                    break;
                case RecurrenceUnit.Days:
                    quest.Schedule.StartDate = quest.Schedule.StartDate.AddDays((int)timeElapsed.TotalDays - ((int)timeElapsed.TotalDays % quest.Schedule.Frequency));
                    break;
                default:
                    break;
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
                                         InitialDirectory = Settings.GetPath(),
                                         RestoreDirectory = false,
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
                                         InitialDirectory = Settings.GetPath(),
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

        private void recurringQuestWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            CheckRecurringQuests();
        }

        private void recurringQuestWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            RenderLog();
        }

        private void questDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                questDescription.SelectAll();
            }
        }

        public void CollapseAllGroups()
        {
            foreach (Control control in quests.Controls)
            {
                if (control.GetType() == typeof(QuestGroupControl))
                {
                    var questGroupControl = (QuestGroupControl)control;

                    questGroupControl.RenderCollapsed();
                }
            }

            forcedCollapse = true;
        }

        public void RestoreAllGroups()
        {
            if (!forcedCollapse)
                return;

            foreach (Control control in quests.Controls)
            {
                if (control.GetType() == typeof(QuestGroupControl))
                {
                    var questGroupControl = (QuestGroupControl)control;

                    questGroupControl.RenderCollapseState();
                }
            }

            forcedCollapse = false;
        }

        public void quests_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestGroupControl)))
            {
                int indicatorY = Cursor.Position.Y - quests.PointToScreen(new Point(0, 0)).Y;
                line.Top = Math.Min(Math.Max((indicatorY + 12) / 24 * 24, 0), questLog.Groups.Count * 24) - 1;
                line.Visible = true;
                line.BringToFront();
            }
        }

        private void quests_DragLeave(object sender, EventArgs e)
        {
            line.Visible = false;
        }

        public void quests_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestGroupControl)))
            {
                var data = (QuestGroupControl)e.Data.GetData(typeof(QuestGroupControl));

                int questScrollOffset = quests.VerticalScroll.Value;

                if (data == null)
                    return;

                var tempQuestControls = new List<QuestGroupControl>();
                var controlY = 0;
                for (int i = quests.Controls.Count - 1; i >= 0; i--)
                {
                    if (quests.Controls[i].GetType() != typeof(QuestGroupControl))
                        continue;

                    var control = (QuestGroupControl)quests.Controls[i];
                    controlY = controlY == 0 ? control.PointToScreen(new Point(0, 0)).Y : controlY + 24;
                    if (controlY > Cursor.Position.Y - 12)
                    {
                        if (control != data)
                            tempQuestControls.Add(control);
                    }
                }

                questLog.Groups.Remove(data.QuestGroup);
                RenderLog();

                foreach (QuestGroupControl control in tempQuestControls)
                {
                    questLog.Groups.Remove(control.QuestGroup);
                }

                questLog.Groups.Add(data.QuestGroup);

                foreach (QuestGroupControl control in tempQuestControls)
                {
                    questLog.Groups.Add(control.QuestGroup);
                }

                line.Visible = false;

                RenderLog();
                LastSelectedQuestGroup = data.QuestGroup;

                var questControl = from Control control in quests.Controls.Cast<Control>()
                                   where control.GetType() == typeof(QuestGroupControl) && ((QuestGroupControl)control).QuestGroup == data.QuestGroup
                                   select (QuestGroupControl)control;

                questControl.First().Focus();
                questControl.First().selected.Checked = data.selected.Checked;

                quests.VerticalScroll.Value = Math.Min(questScrollOffset, quests.VerticalScroll.Maximum);
            }
        }

        private void quests_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestGroupControl)))
                e.Effect = DragDropEffects.Move;
        }
    }
}
