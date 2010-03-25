using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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

            QuestGroup newQuestGroup = new QuestGroup();

            mainForm.questLog.Groups.Add(newQuestGroup);

            if (mainForm.lastSelectedQuestControl != null)
                mainForm.lastSelectedQuestControl.QuestControl_Leave(sender, e);

            mainForm.lastSelectedQuestGroup = newQuestGroup;

            mainForm.RenderLog();

            if (mainForm.lastSelectedQuestGroupControl != null)
                mainForm.lastSelectedQuestGroupControl.name_DoubleClick(sender, e);
        }
    }
}
