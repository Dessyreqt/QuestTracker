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

            if (parentQuestGroupControl == null)
                throw new Exception("Could not identify parent quest group control.");

            parentQuestGroupControl.AddNewQuest(sender, e);
        }
    }
}
