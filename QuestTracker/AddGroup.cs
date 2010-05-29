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
            var mainForm = GetMainForm();

            var newQuestGroup = new QuestGroup();

            mainForm.questLog.Groups.Add(newQuestGroup);

            if (mainForm.lastSelectedQuestControl != null)
                mainForm.lastSelectedQuestControl.QuestControl_Leave(sender, e);

            mainForm.RenderLog();

            mainForm.LastSelectedQuestGroup = newQuestGroup;

            if (mainForm.lastSelectedQuestGroupControl != null)
                mainForm.lastSelectedQuestGroupControl.name_DoubleClick(sender, e);
        }

        private MainForm GetMainForm()
        {
            var retVal = (MainForm)ParentForm;

            if (retVal == null) throw new Exception("Could not identify main form.");

            return retVal;
        }

        private void AddGroup_DragDrop(object sender, DragEventArgs e)
        {
            GetMainForm().quests_DragDrop(sender, e);
        }

        private void AddGroup_DragOver(object sender, DragEventArgs e)
        {
            GetMainForm().quests_DragOver(sender, e);
        }

        private void AddGroup_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(QuestGroupControl)))
                e.Effect = DragDropEffects.Move;
        }
    }
}
