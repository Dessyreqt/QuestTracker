using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuestTracker
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

            var mainForm = (MainForm)ParentForm;

            if (mainForm == null) 
                throw new Exception("Could not identify main form.");

            mainForm.startDate.Text = "Date Started: " + quest.StartDate;
            mainForm.completeDate.Visible = true;
            if (quest.Completed)
            {
                mainForm.completeDate.ForeColor = Color.Green;
                mainForm.completeDate.Text = "Date Completed: " + quest.CompleteDate;
            }
            else
            {
                mainForm.completeDate.ForeColor = Color.Red;
                mainForm.completeDate.Text = "Not Completed";
            }

            mainForm.questDescription.Enabled = true;
            mainForm.questDescription.Lines = quest.Description.Split(new[]{"\r\n","\r", "\n"}, StringSplitOptions.None);

            if (mainForm.lastSelectedQuestControl != null && mainForm.lastSelectedQuestControl != this)
                mainForm.lastSelectedQuestControl.SetNormalBackcolor();

            mainForm.lastSelectedQuest = quest;
            mainForm.lastSelectedQuestControl = this;
            mainForm.lastSelectedQuestGroup = ((QuestGroupControl)Parent).QuestGroup;
            mainForm.lastSelectedQuestGroupControl = (QuestGroupControl)Parent;

            mainForm.SetSelectionPlurality();
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

        private void name_Click(object sender, EventArgs e)
        {
            name.Focus();
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

            var mainForm = (MainForm)ParentForm;

            if (mainForm == null) throw new Exception("Could not identify main form.");

            mainForm.SetSelectionPlurality();
        }

        private void rename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                rename.Text = currentName;
                rename.Visible = false;
            }
        }
    }
}
