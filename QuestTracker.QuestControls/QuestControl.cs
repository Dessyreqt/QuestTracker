using System;
using System.Drawing;
using System.Windows.Forms;
using QuestTracker.Data;

namespace QuestTracker.QuestControls
{
    public partial class QuestControl : UserControl
    {
        private Quest quest;
        private string currentName;

        public Quest Quest
        {
            get { return quest; }
            set
            {
                quest = value;
                name.Text = quest.Name;
            }
        }

        public QuestControl()
        {
            InitializeComponent();
        }


        private void QuestControl_Enter(object sender, EventArgs e)
        {
            SetHighlightedBackcolor();

            var questLog = QuestLogControl.GetQuestLog(this);

            if (questLog.lastSelectedQuestControl != null && questLog.lastSelectedQuestControl != this)
                questLog.lastSelectedQuestControl.SetNormalBackcolor();

            questLog.LastSelectedQuest = quest;
            questLog.lastSelectedQuestControl = this;
            questLog.LastSelectedQuestGroup = ((QuestGroupControl)Parent).QuestGroup;
            questLog.lastSelectedQuestGroupControl = (QuestGroupControl)Parent;

            questLog.ChangeSelectedQuest();
            questLog.SetSelectionPlurality();
        }

        public void SetHighlightedBackcolor()
        {
            name.BackColor = SystemColors.MenuHighlight;
            selected.BackColor = SystemColors.MenuHighlight;
            filler.BackColor = SystemColors.MenuHighlight;
        }

        public void QuestControl_Leave(object sender, EventArgs e)
        {
            SetNormalBackcolor();
            rename.Visible = false;
        }

        public void SetNormalBackcolor()
        {
            if (quest.Completed)
            {
                name.BackColor = Color.LightGreen;
                selected.BackColor = Color.LightGreen;
                filler.BackColor = Color.LightGreen;
            }
            else
            {
                name.BackColor = SystemColors.Window;
                selected.BackColor = SystemColors.Window;
                filler.BackColor = SystemColors.Window;
            }
        }

        public void name_DoubleClick(object sender, EventArgs e)
        {
            rename.Width = Width - rename.Left - 2;
            rename.Text = name.Text;
            currentName = name.Text;
            rename.Visible = true;
            rename.SelectAll();
            rename.Focus();
        }

        private void QuestControl_Resize(object sender, EventArgs e)
        {
            rename.Width = Width - rename.Left - 2;
        }

        private void filler_Click(object sender, EventArgs e)
        {
            filler.Focus();
        }

        private void rename_TextChanged(object sender, EventArgs e)
        {
            name.Text = rename.Text.Trim().Replace("\r", "").Replace("\n", "");
            quest.Name = name.Text;
        }

        private void rename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                rename.SelectionLength = 0;
                rename.Visible = false;
                name.Focus();
            }
        }

        private void selected_CheckedChanged(object sender, EventArgs e)
        {
            if (selected.Focused)
            {
                bool allQuestsChecked = true;

                foreach (Control questControl in Parent.Controls)
                {
                    if (questControl.GetType() == typeof (QuestControl))
                    {
                        allQuestsChecked &= ((QuestControl)questControl).selected.Checked;
                        if (!allQuestsChecked) break;
                    }
                }

                ((QuestGroupControl)Parent).selected.Checked &= allQuestsChecked;
            }

            QuestLogControl.GetQuestLog(this).SetSelectionPlurality();
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

        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mainForm = QuestLogControl.GetQuestLog(this);

            if (mainForm.lastSelectedQuestGroupControl !=  null)
                mainForm.lastSelectedQuestGroupControl.QuestGroupControl_Leave(sender, e);

            QuestControl_Enter(sender, e);
        }

        private void makeQuestRecurring_Click(object sender, EventArgs e)
        {
            var recurringQuestDialog = new RecurringQuestDialog(quest);

            recurringQuestDialog.ShowDialog(ParentForm);
        }

        private void QuestControl_MouseMove(object sender, MouseEventArgs e)
        {
            if ((Cursor.Position.Y < PointToScreen(new Point(0, 0)).Y || Cursor.Position.Y > PointToScreen(new Point(0, 23)).Y) && MouseButtons == MouseButtons.Left)
            {
                Focus();
                DoDragDrop(this, DragDropEffects.Move);
            }
        }

        private void name_Click(object sender, EventArgs e)
        {
            name.Focus();
        }

        private void QuestControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                name_DoubleClick(sender, e);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                var questLog = QuestLogControl.GetQuestLog(this);
                questLog.DeleteQuests();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                var parentQuestGroupControl = (QuestGroupControl)Parent;

                if (parentQuestGroupControl == null)
                    throw new Exception("Could not identify parent quest group control.");

                parentQuestGroupControl.AddNewQuest(sender, e);
            }
        }
    }
}