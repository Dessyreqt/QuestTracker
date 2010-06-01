﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using QuestTracker.Data;
using QuestTracker.IO;

namespace QuestTracker.QuestControls
{
    public partial class QuestLogControl : UserControl
    {
        public QuestLog questLog;
        public QuestGroupControl lastSelectedQuestGroupControl;
        public QuestControl lastSelectedQuestControl;
        public readonly List<QuestControl> questControls;
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

        public QuestLogControl()
        {
            InitializeComponent();

            questControls = new List<QuestControl>();
            questGroupControls = new List<QuestGroupControl>();
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

        private void recurringQuestWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            CheckRecurringQuests();
        }

        private void recurringQuestWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
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
                var questGroupControl = new QuestGroupControl { Dock = DockStyle.Top, QuestGroup = questGroup, TabStop = false, ShowCompleted = showCompleted};

                quests.Controls.Add(questGroupControl);
                questGroupControls.Add(questGroupControl);
                questGroupControl.BringToFront();
            }

            foreach (QuestGroupControl control in questGroupControls)
            {
                if (control.GetType() == typeof(QuestGroupControl))
                {
                    var questGroupControl = control;
                    questGroupControl.ShowCompleted = showCompleted;
                    questGroupControl.RenderGroup();
                }
            }

            if (!addGroupPresent)
            {
                var addGroup = new AddGroupControl { Dock = DockStyle.Top, TabStop = false };
                quests.Controls.Add(addGroup);
                addGroupPresent = true;
            }

            FixZOrder();

            var addGroupQuery = from Control addGroup in quests.Controls.Cast<Control>()
                                where addGroup.GetType() == typeof(AddGroupControl)
                                select (AddGroupControl)addGroup;

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

        public void CompleteQuests()
        {
            if (AnyChecked)
            {
                if (AllCheckedComplete)
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
            if (lastSelectedQuestControl != null)
                lastSelectedQuestControl.Focus();
            else if (lastSelectedQuestGroupControl != null) 
                lastSelectedQuestGroupControl.Focus();
        }

        public void DeleteQuests()
        {
            if (MessageBox.Show("Are you sure you wish to delete the selected quests?\n\nA backup of the current file will be made if you choose to delete.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            if (AnyChecked)
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

        public void SetSelectionPlurality()
        {
            AnyChecked = false;
            AllCheckedComplete = true;

            foreach (var questControl in questControls)
            {
                if (questControl.selected.Checked)
                {
                    AnyChecked = true;
                    if (!questControl.Quest.Completed)
                        AllCheckedComplete = false;
                }
            }

            foreach (var questGroupControl in questGroupControls)
            {
                if (questGroupControl.selected.Checked)
                {
                    AnyChecked = true;
                }
            }

            if (SelectionPluralityChanged != null)
                SelectionPluralityChanged(this, EventArgs.Empty);
        }

        public static QuestLogControl GetQuestLog(Control control)
        {
            Control retVal = control.Parent;

            if (retVal == null) throw new Exception("Could not identify quest log.");

            while (retVal.GetType() != typeof(QuestLogControl) && retVal.Parent != null)
            {
                retVal = retVal.Parent;
            }

            return (QuestLogControl)retVal;
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

        private void QuestLog_Resize(object sender, EventArgs e)
        {
            quests.Width = Width;
            quests.Height = Height;
        }
    }
}
