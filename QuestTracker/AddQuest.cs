using System;
using System.Windows.Forms;

namespace QuestTracker
{
    public partial class AddQuest : UserControl
    {
        public AddQuest()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            var parentQuestGroupControl = (QuestGroupControl)Parent;
            var mainForm = (MainForm)ParentForm;

            if (parentQuestGroupControl == null)
                throw new Exception("Could not identify parent quest group control.");

            if (mainForm == null)
                throw new Exception("Could not identify main form.");

            var newQuest = new Quest();
            
            parentQuestGroupControl.QuestGroup.Quests.Add(newQuest);

            parentQuestGroupControl.RenderGroup();

            if (mainForm.lastSelectedQuestControl != null)
                mainForm.lastSelectedQuestControl.QuestControl_Leave(sender, e);

            mainForm.LastSelectedQuest = newQuest;
            
            if (mainForm.lastSelectedQuestControl != null)
                mainForm.lastSelectedQuestControl.name_DoubleClick(sender, e);
        }
    }
}
