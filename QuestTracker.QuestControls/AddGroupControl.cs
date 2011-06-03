using System;
using System.Windows.Forms;
using QuestTracker.Data;

namespace QuestTracker.QuestControls
{
    public partial class AddGroupControl : UserControl
    {
        public AddGroupControl()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            var questTab = QuestTabControl.GetQuestTab(this);

            var newQuestGroup = new QuestGroup();

            questTab.QuestTab.Groups.Add(newQuestGroup);

            if (questTab.LastSelectedQuestControl != null)
                questTab.LastSelectedQuestControl.QuestControl_Leave(sender, e);

            questTab.RenderTab();

            questTab.LastSelectedQuestGroup = newQuestGroup;

            if (questTab.LastSelectedQuestGroupControl != null)
                questTab.LastSelectedQuestGroupControl.name_DoubleClick(sender, e);
        }

        private void AddGroup_DragDrop(object sender, DragEventArgs e)
        {
            QuestTabControl.GetQuestTab(this).quests_DragDrop(sender, e);
        }

        private void AddGroup_DragOver(object sender, DragEventArgs e)
        {
            QuestTabControl.GetQuestTab(this).quests_DragOver(sender, e);
        }

        private void AddGroup_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestGroupControl)))
                e.Effect = DragDropEffects.Move;
        }
    }
}