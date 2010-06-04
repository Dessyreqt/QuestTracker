using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using QuestTracker.Data;
using QuestTracker.IO;
using QuestTracker.QuestControls.Properties;

namespace QuestTracker.QuestControls
{
    public partial class QuestLogControl : UserControl
    {
        public QuestLog QuestLog { get; set; }
        public QuestGroupControl LastSelectedQuestGroupControl { get; set; }
        public QuestControl LastSelectedQuestControl { get; set; }
        public List<QuestControl> QuestControls { get; private set; }
        public bool AnyChecked { get; set; }
        public bool AllCheckedComplete { get; set; }

        public event EventHandler SelectionPluralityChanged;
        public event EventHandler QuestSelectionChanged;

        private readonly List<QuestGroupControl> questGroupControls;
        private QuestGroup lastSelectedQuestGroup;
        private Quest lastSelectedQuest;
        private bool addGroupPresent;
        private bool lastShowCompleted;
        private bool forcedCollapse;

        public QuestGroup LastSelectedQuestGroup
        {
            get { return lastSelectedQuestGroup; }
            set
            {
                lastSelectedQuestGroup = value;

                if (value == null)
                    return;
                
                var lastSelectedQuery = from QuestGroupControl questGroupControl in questGroupControls
                                        where questGroupControl.QuestGroup == lastSelectedQuestGroup
                                        select questGroupControl;

                LastSelectedQuestGroupControl = lastSelectedQuery.First();
            }
        }

        public Quest LastSelectedQuest
        {
            get { return lastSelectedQuest; }
            set
            {
                lastSelectedQuest = value;

                if (value == null)
                    return;
                
                var lastSelectedQuery = from QuestControl questControl in QuestControls
                                        where questControl.Quest == lastSelectedQuest
                                        select questControl;

                LastSelectedQuestControl = lastSelectedQuery.First();
            }
        }

        public QuestLogControl()
        {
            InitializeComponent();

            QuestControls = new List<QuestControl>();
            questGroupControls = new List<QuestGroupControl>();
        }

        public void quests_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(QuestGroupControl)))
                return;
            
            var indicatorY = Cursor.Position.Y - quests.PointToScreen(new Point(0, 0)).Y;
            line.Top = Math.Min(Math.Max((indicatorY + 12) / 24 * 24, 0), QuestLog.Groups.Count * 24) - 1;
            line.Width = Width;
            line.Visible = true;
            line.BringToFront();
        }

        private void quests_DragLeave(object sender, EventArgs e)
        {
            line.Visible = false;
        }

        public void quests_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(QuestGroupControl)))
                return;
            
            var data = (QuestGroupControl)e.Data.GetData(typeof(QuestGroupControl));

            var questScrollOffset = quests.VerticalScroll.Value;

            if (data == null)
                return;

            var tempQuestControls = new List<QuestGroupControl>();
            var controlY = 0;
            for (var i = quests.Controls.Count - 1; i >= 0; i--)
            {
                if (!(quests.Controls[i] is QuestGroupControl))
                    continue;

                var control = (QuestGroupControl)quests.Controls[i];
                controlY = controlY == 0 ? control.PointToScreen(new Point(0, 0)).Y : controlY + 24;
                
                if (controlY <= Cursor.Position.Y - 12)
                    continue;
                
                if (control != data)
                    tempQuestControls.Add(control);
            }

            QuestLog.Groups.Remove(data.QuestGroup);

            foreach (var control in tempQuestControls)
            {
                QuestLog.Groups.Remove(control.QuestGroup);
            }

            QuestLog.Groups.Add(data.QuestGroup);

            foreach (var control in tempQuestControls)
            {
                QuestLog.Groups.Add(control.QuestGroup);
            }

            line.Visible = false;

            RenderLog();
   
            LastSelectedQuestGroup = data.QuestGroup;

            var questGroupControl = (from Control control in quests.Controls.Cast<Control>()
                                     where control is QuestGroupControl && ((QuestGroupControl)control).QuestGroup == data.QuestGroup
                                     select (QuestGroupControl)control).First();

            if (LastSelectedQuestControl != null)
            {
                LastSelectedQuestControl = (from Control control in questGroupControl.Controls
                                            where control is QuestControl && ((QuestControl)control).Quest == LastSelectedQuest
                                            select (QuestControl)control).First();

                LastSelectedQuestControl.SetHighlightedBackcolor();
                data.SetNormalBackcolor();
            }

            SelectLastSelected();
            questGroupControl.selected.Checked = data.selected.Checked;

            quests.VerticalScroll.Value = Math.Min(questScrollOffset, quests.VerticalScroll.Maximum);
        }

        private void quests_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestGroupControl)))
                e.Effect = DragDropEffects.Move;
        }

        private void recurringQuestWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CheckRecurringQuests();
        }

        private void recurringQuestWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RenderLog();
        }

        public void ChangeSelectedQuest()
        {
            if (QuestSelectionChanged != null)
                QuestSelectionChanged(this, EventArgs.Empty);
        }

        private void CheckRecurringQuests()
        {
            while (true)
            {
                if (QuestLog != null)
                {
                    var refreshLog = false;

                    foreach (var group in QuestLog.Groups)
                    {
                        foreach (var quest in group.Quests.Where(quest => quest.Recurring).Where(quest => quest.Completed).Where(ShouldRecurQuest))
                        {
                            RecurQuest(quest);
                            refreshLog = true;
                        }
                    }

                    if (refreshLog)
                        recurringQuestWorker.ReportProgress(0);
                }

                Thread.Sleep(5000);
            }
        }

        private static bool ShouldRecurQuest(Quest quest)
        {
            var minuteRecurTime = quest.Schedule.Unit == RecurrenceUnit.Minutes && quest.Schedule.StartDate.AddMinutes(quest.Schedule.Frequency) < DateTime.Now;
            var hourRecurTime = quest.Schedule.Unit == RecurrenceUnit.Hours && quest.Schedule.StartDate.AddHours(quest.Schedule.Frequency) < DateTime.Now;
            var dayRecurTime = quest.Schedule.Unit == RecurrenceUnit.Days && quest.Schedule.StartDate.AddDays(quest.Schedule.Frequency) < DateTime.Now;
            
            return minuteRecurTime || hourRecurTime || dayRecurTime;
        }

        private static void RecurQuest(Quest quest)
        {
            quest.Completed = false;
            SetNextRecurDate(quest);
        }

        private static void SetNextRecurDate(Quest quest)
        {
            if (!quest.Recurring) return;

            var timeElapsed = DateTime.Now - quest.Schedule.StartDate;

            switch (quest.Schedule.Unit)
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

        public void RenderLog()
        {
            RenderLog(lastShowCompleted);
        }

        public void RenderLog(bool showCompleted)
        {
            lastShowCompleted = showCompleted;
            var questScrollOffset = quests.VerticalScroll.Value;

            //remove groups that aren't in the log
            var controlsToDelete = (from Control questGroupControl in quests.Controls.Cast<Control>()
                                    where questGroupControl is QuestGroupControl && !QuestLog.Groups.Contains(((QuestGroupControl)questGroupControl).QuestGroup)
                                    select (QuestGroupControl)questGroupControl).ToList();

            foreach (var questGroupControl in controlsToDelete)
            {
                foreach (var quest in questGroupControl.QuestControls)
                {
                    QuestControls.Remove(quest);
                }

                quests.Controls.Remove(questGroupControl);
                questGroupControls.Remove(questGroupControl);
            }

            //add new groups that are in the log
            var questGroupsInControls = from Control questGroupControl in quests.Controls.Cast<Control>()
                                        where questGroupControl is QuestGroupControl
                                        select ((QuestGroupControl)questGroupControl).QuestGroup;

            var questGroupsToAdd = from QuestGroup questGroup in QuestLog.Groups
                                   where !questGroupsInControls.Contains(questGroup)
                                   select questGroup;

            foreach (var questGroupControl in questGroupsToAdd.Select(questGroup => new QuestGroupControl {Dock = DockStyle.Top, QuestGroup = questGroup, TabStop = false, ShowCompleted = showCompleted}))
            {
                quests.Controls.Add(questGroupControl);
                questGroupControls.Add(questGroupControl);
                questGroupControl.BringToFront();
            }

            foreach (var control in questGroupControls)
            {
                var questGroupControl = control;
                questGroupControl.ShowCompleted = showCompleted;
                questGroupControl.RenderGroup();
            }

            if (!addGroupPresent)
            {
                var addGroup = new AddGroupControl { Dock = DockStyle.Top, TabStop = false };
                quests.Controls.Add(addGroup);
                addGroupPresent = true;
            }

            FixZOrder();

            var addGroupControl = (from Control addGroup in quests.Controls.Cast<Control>()
                                   where addGroup is AddGroupControl
                                   select (AddGroupControl)addGroup).First();

            addGroupControl.BringToFront();

            quests.VerticalScroll.Value = Math.Min(questScrollOffset, quests.VerticalScroll.Maximum);
            quests.Refresh();
        }

        private void FixZOrder()
        {
            var questControlsOrdered = (from QuestGroupControl questGroupControl in quests.Controls.OfType<QuestGroupControl>()
                                        orderby QuestLog.Groups.IndexOf(questGroupControl.QuestGroup) descending
                                        select questGroupControl).ToArray();

            var i = -1;

            foreach (var questGroupControl in questControlsOrdered)
            {
                do
                {
                    i++;
                } while (!(quests.Controls[i] is QuestGroupControl));

                if (quests.Controls[i] != questGroupControl)
                    quests.Controls.SetChildIndex(questGroupControl, i);
            }
        }

        public void CompleteQuests()
        {
            if (AnyChecked)
            {
                if (AllCheckedComplete)
                {
                    foreach (var questControl in QuestControls.Where(questControl => questControl.selected.Checked))
                    {
                        questControl.Quest.Completed = false;
                        questControl.Quest.CompleteDates.RemoveAt(questControl.Quest.CompleteDates.Count - 1);
                        questControl.selected.Checked = false;
                    }
                }
                else
                {
                    foreach (var questControl in QuestControls)
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
                }

                RenderLog();
                SelectLastSelected();

                foreach (var questGroupControl in questGroupControls)
                {
                    questGroupControl.selected.Checked = false;
                }
            }
            else
            {
                if (LastSelectedQuest != null)
                {
                    if (LastSelectedQuest.Completed)
                    {
                        LastSelectedQuest.Completed = false;
                        LastSelectedQuest.CompleteDates.RemoveAt(LastSelectedQuest.CompleteDates.Count - 1);
                    }
                    else
                    {
                        LastSelectedQuest.Completed = true;
                        LastSelectedQuest.CompleteDates.Add(DateTime.Now);
                        SetNextRecurDate(LastSelectedQuest);
                    }
                    
                    RenderLog();
                    SelectLastSelected();
                }
            }

            SetSelectionPlurality();
        }

        public void SelectLastSelected()
        {
            if (LastSelectedQuestControl != null)
                LastSelectedQuestControl.Focus();
            else if (LastSelectedQuestGroupControl != null) 
                LastSelectedQuestGroupControl.Focus();
        }

        public void DeleteQuests()
        {
            if (MessageBox.Show(Resources.DeleteQuestsDialogText, Resources.DeleteQuestsDialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            if (AnyChecked)
            {
                FileWriter.Export(QuestLog);

                foreach (var questGroup in questGroupControls)
                {
                    foreach (var questControl in questGroup.QuestControls.Where(questControl => questControl.selected.Checked))
                    {
                        questGroup.QuestGroup.Quests.Remove(questControl.Quest);
                    }

                    if (questGroup.selected.Checked)
                        QuestLog.Groups.Remove(questGroup.QuestGroup);
                }

                RenderLog();
            }
            else
            {
                if (lastSelectedQuest != null)
                {
                    FileWriter.Export(QuestLog);

                    lastSelectedQuestGroup.Quests.Remove(lastSelectedQuest);

                    RenderLog();
                }
            }

            SetSelectionPlurality();
        }

        public void SetSelectionPlurality()
        {
            AnyChecked = false;
            AllCheckedComplete = true;

            foreach (var questControl in QuestControls.Where(questControl => questControl.selected.Checked))
            {
                AnyChecked = true;
                if (!questControl.Quest.Completed)
                    AllCheckedComplete = false;
            }

            AnyChecked |= questGroupControls.Where(questGroupControl => questGroupControl.selected.Checked).Any();

            if (SelectionPluralityChanged != null)
                SelectionPluralityChanged(this, EventArgs.Empty);
        }

        public static QuestLogControl GetQuestLog(Control control)
        {
            var retVal = control.Parent;

            if (retVal == null) throw new Exception("Could not identify quest log.");

            while (!(retVal is QuestLogControl) && retVal.Parent != null)
            {
                retVal = retVal.Parent;
            }

            return (QuestLogControl)retVal;
        }

        public void CollapseAllGroups()
        {
            foreach (var questGroupControl in quests.Controls.OfType<QuestGroupControl>())
            {
                questGroupControl.RenderCollapsed();
            }

            quests.VerticalScroll.Value = 0;
            forcedCollapse = true;
        }

        public void RestoreAllGroups()
        {
            if (!forcedCollapse)
                return;

            foreach (var questGroupControl in quests.Controls.OfType<QuestGroupControl>())
            {
                questGroupControl.RenderCollapseState();
            }

            forcedCollapse = false;
        }
    }
}
