using System;
using System.Windows.Forms;

namespace QuestTracker
{
    public partial class AddGroup : UserControl
    {
        public AddGroup()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            var mainForm = (MainForm)ParentForm;

            if (mainForm == null)
                throw new Exception("Could not identify main form.");

            var newQuestGroup = new QuestGroup();

            mainForm.questLog.Groups.Add(newQuestGroup);

            if (mainForm.lastSelectedQuestControl != null)
                mainForm.lastSelectedQuestControl.QuestControl_Leave(sender, e);

            mainForm.RenderLog();

            mainForm.LastSelectedQuestGroup = newQuestGroup;

            if (mainForm.lastSelectedQuestGroupControl != null)
                mainForm.lastSelectedQuestGroupControl.name_DoubleClick(sender, e);
        }
    }
}
