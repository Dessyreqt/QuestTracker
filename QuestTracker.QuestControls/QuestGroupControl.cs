﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuestTracker.Data;

namespace QuestTracker.QuestControls
{
    public partial class QuestGroupControl : UserControl
    {
        private bool collapsed;
        private QuestGroup questGroup;
        private string currentName;

        public List<QuestControl> QuestControls { get; set; }
        public bool ShowCompleted { get; set; }

        public QuestGroup QuestGroup
        {
            get { return questGroup; }
            set
            {
                questGroup = value;
                SetQuestName(true);
                collapsed = questGroup.Collapsed;
            }
        }

        public QuestGroupControl()
        {
            InitializeComponent();
            collapsed = false;
            questGroup = new QuestGroup();
            QuestControls = new List<QuestControl>();
        }

        private void SetQuestName(bool showCount)
        {
            var completedQuests = from Quest quest in questGroup.Quests
                                  where quest.Completed
                                  select quest;

            var questCount = new StringBuilder();
            if (showCount)
            {
                questCount.Append(" (");
                questCount.Append(completedQuests.Count());
                questCount.Append(" completed of ");
                questCount.Append(questGroup.Quests.Count);
                questCount.Append(")");
            }

            name.Text = questGroup.Name + questCount;
        }

        public void RenderCollapseState()
        {
            if (collapsed)
            {
                RenderCollapsed();
            }
            else
            {
                RenderExpanded();
            }
        }

        public void RenderCollapsed()
        {
            Height = 24;
            addQuest.Visible = false;   //otherwise addQuest will be the only visible control in the group.
            expand.Image = Properties.Resources.expand;
        }

        private void RenderExpanded()
        {
            addQuest.Visible = true;
            expand.Image = Properties.Resources.collapse;
            SetHeight();
        }

        private void SetHeight()
        {
            var newHeight = 48 + Controls.OfType<QuestControl>().Where(control => control.Visible).Sum(control => 24);

            Height = newHeight;
        }

        private void RenderCompletionBased()
        {
            foreach (var questControl in Controls.OfType<QuestControl>())
            {
                questControl.SetNormalBackcolor();

                if (questControl.Quest.Completed)
                {
                    questControl.Visible = ShowCompleted;
                }
            }
        }

        private void AddQuestControl(QuestControl questControl)
        {
            var questLog = QuestTabControl.GetQuestTab(this);

            questControl.Dock = DockStyle.Top;
            questControl.SetNormalBackcolor();

            if (questControl.Quest.Completed)
            {
                questControl.Visible = ShowCompleted;
            }

            Controls.Add(questControl);
            QuestControls.Add(questControl);
            questLog.QuestControls.Add(questControl);
            questControl.BringToFront();
        }

        private void QuestGroupControl_Enter(object sender, EventArgs e)
        {
            SetHighlightedBackcolor();

            if (ParentForm == null)
                return;

            var questLog = QuestTabControl.GetQuestTab(this);

            questLog.RestoreAllGroups();

            if (questLog.LastSelectedQuestControl != null)
                questLog.LastSelectedQuestControl.SetNormalBackcolor();

            questLog.LastSelectedQuest = null;
            questLog.LastSelectedQuestControl = null;
            questLog.LastSelectedQuestGroup = questGroup;
            questLog.LastSelectedQuestGroupControl = this;

            questLog.ChangeSelectedQuest();
        }

        private void SetHighlightedBackcolor()
        {
            name.BackColor = SystemColors.Highlight;
            selected.BackColor = SystemColors.Highlight;
            expand.BackColor = SystemColors.Highlight;
            name.ForeColor = SystemColors.HighlightText;
        }

        public void QuestGroupControl_Leave(object sender, EventArgs e)
        {
            SetNormalBackcolor();
            rename.Visible = false;
        }

        public void SetNormalBackcolor()
        {
            name.BackColor = SystemColors.Control;
            selected.BackColor = SystemColors.Control;
            expand.BackColor = SystemColors.Control;
            name.ForeColor = SystemColors.ControlText;
        }

        private void name_Click(object sender, EventArgs e)
        {
            name.Focus();
        }

        private void panel_Resize(object sender, EventArgs e)
        {
            if (rename.Visible)
                rename.Width = Width - rename.Left - 2;
        }

        public void name_DoubleClick(object sender, EventArgs e)
        {
            StartRenameGroup();
        }

        private void StartRenameGroup()
        {
            rename.Width = Width - rename.Left - 2;
            rename.Text = questGroup.Name;
            currentName = questGroup.Name;
            rename.Visible = true;
            rename.SelectAll();
            rename.Focus();
        }

        private void selected_CheckedChanged(object sender, EventArgs e)
        {
            if (selected.Focused)
            {
                foreach (var questControl in Controls.OfType<QuestControl>())
                {
                    questControl.selected.Checked = selected.Checked;
                }
            }

            var mainForm = QuestTabControl.GetQuestTab(this);

            mainForm.SetSelectionPlurality();
        }

        private void expand_Click(object sender, EventArgs e)
        {
            expand.Focus();
            collapsed = !collapsed;
            questGroup.Collapsed = collapsed;

            RenderCollapseState();
        }

        private void rename_TextChanged(object sender, EventArgs e)
        {
            questGroup.Name = rename.Text.Trim().Replace("\r", "").Replace("\n", "");
        }

        private void rename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;
            
            rename.SelectionLength = 0;
            rename.Visible = false;
            SetQuestName(true);
            name.Focus();
        }

        private void rename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape)
                return;
            
            rename.Text = currentName;
            rename.Visible = false;
            name.Focus();
        }

        private void QuestGroupControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestControl)))
                e.Effect = DragDropEffects.Move;
            else if (e.Data.GetDataPresent(typeof(QuestGroupControl)))
                e.Effect = DragDropEffects.Move;
        }

        private void QuestGroupControl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestControl)))
            {
                var data = (QuestControl)e.Data.GetData(typeof(QuestControl));
                var questTabControl = QuestTabControl.GetQuestTab(this);

                var questScrollOffset = questTabControl.quests.VerticalScroll.Value;

                if (data == null)
                    return;

                var tempQuestControls = new List<QuestControl>();
                for (var i = Controls.Count - 1; i >= 0; i--)
                {
                    if (!(Controls[i] is QuestControl))
                        continue;

                    var control = (QuestControl)Controls[i];
                    if (control.PointToScreen(new Point(0, 0)).Y <= Cursor.Position.Y - 12)
                        continue;
                    
                    if (control != data)
                        tempQuestControls.Add(control);
                }

                var questLogControl = QuestLogControl.GetQuestLog(this);

                questLogControl.LastSelectedQuestGroup.Quests.Remove(data.Quest);

                foreach (var control in tempQuestControls)
                {
                    questGroup.Quests.Remove(control.Quest);
                }

                questGroup.Quests.Add(data.Quest);

                foreach (var control in tempQuestControls)
                {
                    questGroup.Quests.Add(control.Quest);
                }

                line.Visible = false;

                questLogControl.LastSelectedQuestGroupControl.RenderGroup();
                RenderGroup();
                questTabControl.LastSelectedQuest = data.Quest;

                var questControl = (from Control control in Controls.Cast<Control>()
                                    where control is QuestControl && ((QuestControl)control).Quest == data.Quest
                                    select (QuestControl)control).First();

                questControl.Focus();
                questControl.SetHighlightedBackcolor();
                questControl.selected.Checked = data.selected.Checked;

                questTabControl.quests.VerticalScroll.Value = Math.Min(questScrollOffset, questTabControl.quests.VerticalScroll.Maximum);
            }
            else if (e.Data.GetDataPresent(typeof(QuestGroupControl)))
            {
                var questTabControl = QuestTabControl.GetQuestTab(this);

                questTabControl.RestoreAllGroups();
                questTabControl.quests_DragDrop(sender, e);
            }
        }

        private void QuestGroupControl_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestControl)))
            {
                var indicatorY = Cursor.Position.Y - PointToScreen(new Point(0, 0)).Y;
                line.Top = Math.Min(Math.Max((indicatorY + 12) / 24 * 24, 24), Height - 24) - 1;
                line.Width = Width;
                line.Visible = true;
                line.BringToFront();
            }
            QuestTabControl.GetQuestTab(this).quests_DragOver(sender, e);
        }

        private void QuestGroupControl_DragLeave(object sender, EventArgs e)
        {
            line.Visible = false;
        }

        public void RenderGroup()
        {
            SetQuestName(true);

            //remove controls that aren't in the group
            var controlsToDelete = (from Control questControl in Controls.Cast<Control>()
                                    where questControl is QuestControl && !questGroup.Quests.Contains(((QuestControl)questControl).Quest)
                                    select questControl).ToList();

            foreach (QuestControl questControl in controlsToDelete)
            {
                RemoveQuestControl(questControl);
            }

            //add new controls that are in the group
            var questsInControls = from Control questControl in Controls.Cast<Control>()
                                   where questControl is QuestControl
                                   select ((QuestControl)questControl).Quest;

            var questsToAdd = from Quest quest in questGroup.Quests
                              where !questsInControls.Contains(quest)
                              select quest;

            foreach (var questControl in questsToAdd.Select(quest => new QuestControl {Quest = quest, TabStop = false}))
            {
                AddQuestControl(questControl);
            }

            FixZOrder();
            RenderCompletionBased();
            RenderCollapseState();
        }

        private void FixZOrder()
        {
            var questControlsOrdered = (from QuestControl questControl in Controls.OfType<QuestControl>()
                                        orderby questGroup.Quests.IndexOf(questControl.Quest) descending
                                        select questControl).ToArray();

            var i = -1;

            foreach (var questControl in questControlsOrdered)
            {
                do
                {
                    i++;
                } while (!(Controls[i] is QuestControl));

                if (Controls[i] != questControl)
                    Controls.SetChildIndex(questControl, i);
            }
        }

        private void RemoveQuestControl(QuestControl questControl)
        {
            if (questControl.Visible)
                Height -= 24;
            Controls.Remove(questControl);

            QuestControls.Remove(questControl);

            var questLog = QuestTabControl.GetQuestTab(this);
            questLog.QuestControls.Remove(questControl);
        }

        private void QuestGroupControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    name_DoubleClick(sender, e);
                    break;
                case Keys.Delete:
                    {
                        var questLog = QuestLogControl.GetQuestLog(this);

                        selected.Checked = true;
                        foreach (var questControl in QuestControls)
                            questControl.selected.Checked = true;

                        questLog.DeleteQuests();
                    }
                    break;
                case Keys.N:
                    if (e.Control)
                    {
                        AddNewQuest(sender, e);
                    }
                    break;
            }
        }

        public void AddNewQuest(object sender, EventArgs e)
        {
            var newQuest = new Quest();
            questGroup.Quests.Add(newQuest);
            RenderGroup();

            var questLog = QuestTabControl.GetQuestTab(this);

            if (questLog.LastSelectedQuestControl != null)
                questLog.LastSelectedQuestControl.QuestControl_Leave(sender, e);

            questLog.LastSelectedQuest = newQuest;

            if (questLog.LastSelectedQuestControl != null)
                questLog.LastSelectedQuestControl.name_DoubleClick(sender, e);
        }

        private void rename_VisibleChanged(object sender, EventArgs e)
        {
            SetQuestName(true);
        }

        private void QuestGroupControl_MouseMove(object sender, MouseEventArgs e)
        {
            if ((Cursor.Position.Y >= PointToScreen(new Point(0, 0)).Y && Cursor.Position.Y <= PointToScreen(new Point(0, 23)).Y) || MouseButtons != MouseButtons.Left)
                return;
            
            Focus();

            QuestTabControl.GetQuestTab(this).CollapseAllGroups();
            SetHighlightedBackcolor();

            DoDragDrop(this, DragDropEffects.Move);
        }

        private void renameGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartRenameGroup();
        }

        private void timeboxGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var timebox = new Timebox(questGroup);

            timebox.Show(ParentForm);
        }
    }
}