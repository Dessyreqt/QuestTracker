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
            var questLog = QuestLogControl.GetQuestLog(this);

            var newQuestGroup = new QuestGroup();

            questLog.questLog.Groups.Add(newQuestGroup);

            if (questLog.lastSelectedQuestControl != null)
                questLog.lastSelectedQuestControl.QuestControl_Leave(sender, e);

            questLog.RenderLog();

            questLog.LastSelectedQuestGroup = newQuestGroup;

            if (questLog.lastSelectedQuestGroupControl != null)
                questLog.lastSelectedQuestGroupControl.name_DoubleClick(sender, e);
        }

        private void AddGroup_DragDrop(object sender, DragEventArgs e)
        {
            QuestLogControl.GetQuestLog(this).quests_DragDrop(sender, e);
        }

        private void AddGroup_DragOver(object sender, DragEventArgs e)
        {
            QuestLogControl.GetQuestLog(this).quests_DragOver(sender, e);
        }

        private void AddGroup_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestGroupControl)))
                e.Effect = DragDropEffects.Move;
        }
    }
}