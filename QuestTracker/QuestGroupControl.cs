using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuestTracker
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

        private MainForm GetMainForm()
        {
            var retVal = (MainForm)ParentForm;

            if (retVal == null) throw new Exception("Could not identify main form.");

            return retVal;
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

        private void RenderCollapseState()
        {
            if (collapsed)
            {
                Height = 24;
                addQuest.Visible = false;   //otherwise addQuest will be the only visible control in the group.
                expand.Image = Properties.Resources.expand;
            }
            else
            {
                addQuest.Visible = true;
                expand.Image = Properties.Resources.collapse;
                SetHeight();
            }
        }

        private void SetHeight()
        {
            Height = 48;

            foreach (Control control in Controls)
            {
                if (control.GetType() != typeof(QuestControl))
                    continue;

                if (control.Visible)
                    Height += 24;
            }
        }

        private void RenderCompletionBased()
        {
            foreach (Control control in Controls)
            {
                if (control.GetType() != typeof(QuestControl))
                    continue;

                var questControl = (QuestControl)control;

                questControl.SetNormalBackcolor();

                if (questControl.Quest.Completed)
                {
                    if (ShowCompleted)
                    {
                        if (!questControl.Visible)
                        {
                            questControl.Visible = true;
                        }
                    }
                    else
                    {
                        if (questControl.Visible)
                        {
                            questControl.Visible = false;
                        }
                    }
                }
            }

            RenderCollapseState();
        }

        public void AddQuestControl(QuestControl questControl)
        {
            var mainForm = GetMainForm();

            if (!collapsed)
                Height += 24;

            questControl.Dock = DockStyle.Top;
            questControl.SetNormalBackcolor();
            Controls.Add(questControl);
            questControls.Add(questControl);
            mainForm.questControls.Add(questControl);
            questControl.BringToFront();
        }

        private void QuestGroupControl_Enter(object sender, EventArgs e)
        {
            SetHighlightedBackcolor();

            if (ParentForm == null)
                return;
                
            var mainForm = GetMainForm();

            mainForm.startDate.Text = "Date Started: ";
            mainForm.completeDate.Visible = false;

            mainForm.questDescription.Enabled = false;
            mainForm.questDescription.Text = "";

            if (mainForm.lastSelectedQuestControl != null)
                mainForm.lastSelectedQuestControl.SetNormalBackcolor();

            mainForm.LastSelectedQuest = null;
            mainForm.lastSelectedQuestControl = null;
            mainForm.LastSelectedQuestGroup = questGroup;
            mainForm.lastSelectedQuestGroupControl = this;
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
            rename.Width = Width - rename.Left - 2;
            line.Width = Width;
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
                    if (questControl.GetType() == typeof (QuestControl))
                        ((QuestControl)questControl).selected.Checked = selected.Checked;
                }
            }

            var mainForm = GetMainForm();

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
            e.Effect = DragDropEffects.Move;
        }

        private void QuestGroupControl_DragDrop(object sender, DragEventArgs e)
        {
            var data = (QuestControl)e.Data.GetData(typeof(QuestControl));
            var mainForm = GetMainForm();

            int questScrollOffset = mainForm.quests.VerticalScroll.Value;
            
            if (data == null) 
                return;

            var tempQuestControls = new List<QuestControl>();
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i].GetType() != typeof(QuestControl))
                    continue;
                
                var control = (QuestControl)Controls[i];
                if (control.PointToScreen(new Point(0, 0)).Y > Cursor.Position.Y - 12)
                {
                    if (control != data)
                        tempQuestControls.Add(control);
                }
            }

            mainForm.LastSelectedQuestGroup.Quests.Remove(data.Quest);
            mainForm.lastSelectedQuestGroupControl.RenderGroup();

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

            RenderGroup();
            mainForm.LastSelectedQuest = data.Quest;

            var questControl = from Control control in Controls.Cast<Control>()
                               where control.GetType() == typeof(QuestControl) && ((QuestControl)control).Quest == data.Quest
                               select (QuestControl)control;

            questControl.First().Focus();
            questControl.First().selected.Checked = data.selected.Checked; 

            mainForm.quests.VerticalScroll.Value = Math.Min(questScrollOffset, mainForm.quests.VerticalScroll.Maximum);
        }

        private void QuestGroupControl_DragOver(object sender, DragEventArgs e)
        {
            int indicatorY =  Cursor.Position.Y - PointToScreen(new Point(0, 0)).Y;
            line.Top = Math.Min(Math.Max((indicatorY + 12) / 24 * 24, 24), Height - 24) - 1;
            line.Visible = true;
            line.BringToFront();
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
                                   where questControl.GetType() == typeof(QuestControl) && !questGroup.Quests.Contains(((QuestControl)questControl).Quest)
                                   select questControl).ToList();

            foreach (QuestControl questControl in controlsToDelete)
            {
                RemoveQuestControl(questControl);
            }

            //add new controls that are in the group
            var questsInControls = from Control questControl in Controls.Cast<Control>()
                                   where questControl.GetType() == typeof(QuestControl)
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
            RenderCompletionBased();
        }

        private void FixZOrder()
        {
            var controlsToCheck = new List<QuestControl>();

            foreach (Control control in Controls)
            {
                if (control.GetType() != typeof(QuestControl))
                    continue;

                controlsToCheck.Add((QuestControl)control);
            }

            for (int i = 0; i < questGroup.Quests.Count; i++)
            {
                foreach (QuestControl control in controlsToCheck)
                {                                               
                    if (control.Quest == questGroup.Quests[i])
                    {
                        control.BringToFront();
                        break;
                    }
                }
            }
        }

        public void RemoveQuestControl(QuestControl questControl)
        {
            if (questControl.Visible)
                Height -= 24;
            Controls.Remove(questControl);

            questControls.Remove(questControl);

            var mainForm = GetMainForm();
            mainForm.questControls.Remove(questControl);
        }

        private void QuestGroupControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                name_DoubleClick(sender, e);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                var mainForm = GetMainForm();
                
                selected.Checked = true;
                foreach (QuestControl questControl in questControls)
                    questControl.selected.Checked = true;

                mainForm.DeleteQuests();
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

            var mainForm = GetMainForm();

            if (mainForm.lastSelectedQuestControl != null)
                mainForm.lastSelectedQuestControl.QuestControl_Leave(sender, e);

            mainForm.LastSelectedQuest = newQuest;

            if (mainForm.lastSelectedQuestControl != null)
                mainForm.lastSelectedQuestControl.name_DoubleClick(sender, e);
        }

        private void rename_VisibleChanged(object sender, EventArgs e)
        {
            SetQuestName(true);
        }
    }
}
