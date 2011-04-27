using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuestTracker.Data;

namespace QuestTracker.QuestControls
{
    public partial class QuestLogControl : UserControl
    {
        private string currentName;

        public event EventHandler SelectionPluralityChanged;
        public event EventHandler QuestSelectionChanged;

        public QuestLogControl()
        {
            InitializeComponent();
        }

        private void questTabs_Click(object sender, EventArgs e)
        {
            if (questTabs.SelectedTab == addTab)
            {
                var newQuestTabControl = new QuestTabControl { Dock = DockStyle.Fill, QuestLog = new QuestLog() };

                var newTab = new TabPage("New Tab");
                newTab.Controls.Add(newQuestTabControl);
                newQuestTabControl.RenderLog();
                questTabs.TabPages.Insert(questTabs.TabPages.Count - 1, newTab);
                questTabs.SelectTab(newTab);
                questTabs_DoubleClick(sender, e);
            }
        }

        private void questTabs_DoubleClick(object sender, EventArgs e)
        {
            rename.Width = questTabs.GetTabRect(questTabs.SelectedIndex).Width - 2;
            rename.Left = questTabs.GetTabRect(questTabs.SelectedIndex).Left + 3;
            rename.Text = questTabs.SelectedTab.Text;
            currentName = questTabs.SelectedTab.Text;
            rename.Visible = true;
            rename.SelectAll();
            rename.Focus();
        }

        private void rename_TextChanged(object sender, EventArgs e)
        {
            questTabs.SelectedTab.Text = rename.Text.Trim().Replace("\r", "").Replace("\n", "");
            rename.Width = questTabs.GetTabRect(questTabs.SelectedIndex).Width - 2;
        }

        private void rename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;

            rename.SelectionLength = 0;
            rename.Visible = false;
            questTabs.SelectedTab.Focus();
        }

        private void rename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape)
                return;

            rename.Text = currentName;
            rename.Visible = false;
            questTabs.SelectedTab.Focus();
        }

        private void rename_Leave(object sender, EventArgs e)
        {
            rename.Visible = false;
        }

        private void questTabControl_QuestSelectionChanged(object sender, EventArgs e)
        {
            QuestSelectionChanged(this, e);
        }

        private void questTabControl_SelectionPluralityChanged(object sender, EventArgs e)
        {
            SelectionPluralityChanged(this, e);
        }
    }
}
