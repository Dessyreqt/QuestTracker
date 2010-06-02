using System;
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

        public List<QuestControl> questControls;
        public bool ShowCompleted { get; set; }

        public QuestGroup QuestGroup 
        { 
            get { return questGroup; }
            set 
            { 
                questGroup = value;
                SetQuestName(true);
                collapsed = questGroup.collapsed;
            } 
        }
           
        public QuestGroupControl()
        {
            InitializeComponent();
            collapsed = false;
            questGroup = new QuestGroup();
            questControls = new List<QuestControl>();
        }

        public void SetQuestName(bool showCount)
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
             var newHeight = 48;

            foreach (Control control in Controls)
            {
                if (!(control is QuestControl))
                    continue;

                if (control.Visible)
                    newHeight += 24;
            }

            Height = newHeight;
        }

        private void AddQuestControl(QuestControl questControl)
        {
            var questLog = QuestLogControl.GetQuestLog(this);

            questControl.Dock = DockStyle.Top;
            questControl.SetNormalBackcolor();
            
            if (questControl.Quest.Completed)
            {
                if (ShowCompleted)
                {
                    questControl.Visible = true;
                }
                else
                {
                    questControl.Visible = false;
                }
            }

            Controls.Add(questControl);
            questControls.Add(questControl);
            questLog.questControls.Add(questControl);
            questControl.BringToFront();
        }

        private void QuestGroupControl_Enter(object sender, EventArgs e)
        {
            SetHighlightedBackcolor();

            if (ParentForm == null)
                return;

            var questLog = QuestLogControl.GetQuestLog(this);

            questLog.RestoreAllGroups();

            if (questLog.lastSelectedQuestControl != null)
                questLog.lastSelectedQuestControl.SetNormalBackcolor();

            questLog.LastSelectedQuest = null;
            questLog.lastSelectedQuestControl = null;
            questLog.LastSelectedQuestGroup = questGroup;
            questLog.lastSelectedQuestGroupControl = this;

            questLog.ChangeSelectedQuest();
        }

        public void SetHighlightedBackcolor()
        {
            name.BackColor = SystemColors.MenuHighlight;
            selected.BackColor = SystemColors.MenuHighlight;
            expand.BackColor = SystemColors.MenuHighlight;
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
                foreach (Control questControl in Controls)
                {
                    if (questControl is QuestControl)
                        ((QuestControl)questControl).selected.Checked = selected.Checked;
                }
            }

            var mainForm = QuestLogControl.GetQuestLog(this);

            mainForm.SetSelectionPlurality();
        }

        private void expand_Click(object sender, EventArgs e)
        {
            expand.Focus();
            collapsed = !collapsed;
            questGroup.collapsed = collapsed;

            RenderCollapseState();
        }

        private void rename_TextChanged(object sender, EventArgs e)
        {
            questGroup.Name = rename.Text.Trim().Replace("\r", "").Replace("\n", "");
        }

        private void rename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                rename.SelectionLength = 0;
                rename.Visible = false;
                SetQuestName(true);
                name.Focus();
            }
        }

        private void rename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                rename.Text = currentName;
                rename.Visible = false;
                name.Focus();
            }
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
                var questLogControl = QuestLogControl.GetQuestLog(this);

                int questScrollOffset = questLogControl.quests.VerticalScroll.Value;

                if (data == null)
                    return;

                var tempQuestControls = new List<QuestControl>();
                for (int i = Controls.Count - 1; i >= 0; i--)
                {
                    if (!(Controls[i] is QuestControl))
                        continue;

                    var control = (QuestControl)Controls[i];
                    if (control.PointToScreen(new Point(0, 0)).Y > Cursor.Position.Y - 12)
                    {
                        if (control != data)
                            tempQuestControls.Add(control);
                    }
                }

                questLogControl.LastSelectedQuestGroup.Quests.Remove(data.Quest);

                foreach (QuestControl control in tempQuestControls)
                {
                    questGroup.Quests.Remove(control.Quest);
                }

                questGroup.Quests.Add(data.Quest);

                foreach (QuestControl control in tempQuestControls)
                {
                    questGroup.Quests.Add(control.Quest);
                }

                line.Visible = false;

                questLogControl.lastSelectedQuestGroupControl.RenderGroup();
                RenderGroup();
                questLogControl.LastSelectedQuest = data.Quest;

                var questControl = from Control control in Controls.Cast<Control>()
                                   where control is QuestControl && ((QuestControl)control).Quest == data.Quest
                                   select (QuestControl)control;

                questControl.First().Focus();
                questControl.First().selected.Checked = data.selected.Checked;

                questLogControl.quests.VerticalScroll.Value = Math.Min(questScrollOffset, questLogControl.quests.VerticalScroll.Maximum);
            }
            else if (e.Data.GetDataPresent(typeof(QuestGroupControl)))
            {
                QuestLogControl.GetQuestLog(this).RestoreAllGroups();
            }
            QuestLogControl.GetQuestLog(this).quests_DragDrop(sender, e);
        }

        private void QuestGroupControl_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestControl)))
            {
                int indicatorY = Cursor.Position.Y - PointToScreen(new Point(0, 0)).Y;
                line.Top = Math.Min(Math.Max((indicatorY + 12) / 24 * 24, 24), Height - 24) - 1;
                line.Width = Width;
                line.Visible = true;
                line.BringToFront();
            }
            QuestLogControl.GetQuestLog(this).quests_DragOver(sender, e);
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

            foreach (Quest quest in questsToAdd)
            {
                var questControl = new QuestControl { Quest = quest, TabStop = false };

                AddQuestControl(questControl);
            }

            FixZOrder();
            RenderCollapseState();
        }

        private void FixZOrder()
        {
            FixZOrderUp();
            FixZOrderDown();
        }

        private void FixZOrderDown()
        {
            for (int i = Controls.Count - 1; i > -1; i--)
            {
                if (!(Controls[i] is QuestControl))
                    continue;

                var j = GetPrevQuestControlIndex(i);

                var questControl = (QuestControl)Controls[i];
                if (j > -1)
                {
                    var prevQuestControl = (QuestControl)Controls[j];

                    if (questGroup.Quests.IndexOf(questControl.Quest) > questGroup.Quests.IndexOf(prevQuestControl.Quest))
                    {
                        Controls.SetChildIndex(questControl, j);
                    }
                }
            }
        }

        private int GetPrevQuestControlIndex(int i)
        {
            var retVal = i - 1;

            while (retVal > -1 && !(Controls[retVal] is QuestControl))
            {
                retVal--;
                if (retVal == -1)
                    break;
            }

            return retVal;
        }

        private void FixZOrderUp()
        {
            for (int i = 0; i < Controls.Count - 1; i++)
            {
                if (!(Controls[i] is QuestControl))
                    continue;

                var j = GetNextQuestControlIndex(i);

                var questControl = (QuestControl)Controls[i];
                if (j > -1)
                {
                    var nextQuestControl = (QuestControl)Controls[j];

                    if (questGroup.Quests.IndexOf(questControl.Quest) < questGroup.Quests.IndexOf(nextQuestControl.Quest))
                    {
                        Controls.SetChildIndex(questControl,j);
                    }
                }
            }
        }

        private int GetNextQuestControlIndex(int i)
        {
            var retVal = i + 1;

            while (Controls.Count > retVal && !(Controls[retVal] is QuestControl))
            {
                retVal++;
                if (retVal == Controls.Count)
                {
                    retVal = -1;
                    break;
                }
            }

            return retVal;
        }

        private void RemoveQuestControl(QuestControl questControl)
        {
            if (questControl.Visible)
                Height -= 24;
            Controls.Remove(questControl);

            questControls.Remove(questControl);

            var questLog = QuestLogControl.GetQuestLog(this);
            questLog.questControls.Remove(questControl);
        }

        private void QuestGroupControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                name_DoubleClick(sender, e);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                var questLog = QuestLogControl.GetQuestLog(this);
                
                selected.Checked = true;
                foreach (QuestControl questControl in questControls)
                    questControl.selected.Checked = true;

                questLog.DeleteQuests();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                AddNewQuest(sender, e);
            }
        }

        public void AddNewQuest(object sender, EventArgs e)
        {
            var newQuest = new Quest();
            questGroup.Quests.Add(newQuest);
            RenderGroup();

            var questLog = QuestLogControl.GetQuestLog(this);

            if (questLog.lastSelectedQuestControl != null)
                questLog.lastSelectedQuestControl.QuestControl_Leave(sender, e);

            questLog.LastSelectedQuest = newQuest;

            if (questLog.lastSelectedQuestControl != null)
                questLog.lastSelectedQuestControl.name_DoubleClick(sender, e);
        }

        private void rename_VisibleChanged(object sender, EventArgs e)
        {
            SetQuestName(true);
        }

        private void QuestGroupControl_MouseMove(object sender, MouseEventArgs e)
        {
            if ((Cursor.Position.Y < PointToScreen(new Point(0, 0)).Y || Cursor.Position.Y > PointToScreen(new Point(0, 23)).Y) && MouseButtons == MouseButtons.Left)
            {
                Focus();

                QuestLogControl.GetQuestLog(this).CollapseAllGroups();
                SetHighlightedBackcolor();

                DoDragDrop(this, DragDropEffects.Move);
            }
        }
    }
}