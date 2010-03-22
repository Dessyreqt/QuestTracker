using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuestTracker
{
    public partial class QuestGroupControl : UserControl
    {
        private bool collapsed;
        private QuestGroup questGroup;

        public QuestGroup QuestGroup 
        { 
            get { return questGroup; }
            set 
            { 
                questGroup = value;
                name.Text = questGroup.Name;
                collapsed = questGroup.collapsed;

                RenderCollapseState();

                foreach (Quest quest in questGroup.Quests)
                {
                    var questControl = new QuestControl {Quest = quest};

                    AddQuestControl(questControl);
                }
            } 
        }

        private void RenderCollapseState()
        {
            if (collapsed)
            {
                Height = 24;
                expand.Image = Properties.Resources.expand;
            }
            else
            {
                Height = 0;
                foreach (Control control in Controls)
                {
                    if (control.Visible)
                        Height += 24;
                }
                expand.Image = Properties.Resources.collapse;
            }
        }

        public QuestGroupControl()
        {
            InitializeComponent();
            collapsed = false;
            questGroup = new QuestGroup();
        }

        public void AddQuestControl(QuestControl questControl)
        {
            if (!collapsed)
                Height += 24;

            questControl.Dock = DockStyle.Top;
            Controls.Add(questControl);
            questControl.BringToFront();
        }

        private void QuestGroupControl_Enter(object sender, EventArgs e)
        {
            name.BackColor = SystemColors.MenuHighlight;
            selected.BackColor = SystemColors.MenuHighlight;
            expand.BackColor = SystemColors.MenuHighlight;

            var mainForm = (MainForm)ParentForm;

            if (mainForm == null) throw new Exception("Could not identify main form.");

            mainForm.startDate.Text = "Date Started: ";
            mainForm.completeDate.Visible = false;

            mainForm.questDescription.Enabled = false;
            mainForm.questDescription.Text = "";

            mainForm.lastSelectedQuest = null;
            mainForm.lastSelectedQuestGroup = questGroup;
        }

        private void QuestGroupControl_Leave(object sender, EventArgs e)
        {
            name.BackColor = SystemColors.Control;
            selected.BackColor = SystemColors.Control;
            expand.BackColor = SystemColors.Control;
            rename.Visible = false;
        }

        private void name_Click(object sender, EventArgs e)
        {
            name.Focus();
        }

        private void panel_Resize(object sender, EventArgs e)
        {
            rename.Width = Width - rename.Left - 2;
        }

        private void name_DoubleClick(object sender, EventArgs e)
        {
            rename.Width = Width - rename.Left - 2;
            rename.Text = name.Text;
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
            name.Text = rename.Text.Trim().Replace("\r", "").Replace("\n", "");
            questGroup.Name = name.Text;
        }

        private void rename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                rename.SelectionLength = 0;
                rename.Visible = false;
            }
        }
    }
}
