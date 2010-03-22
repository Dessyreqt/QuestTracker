using System;
using System.Threading;
using System.Windows.Forms;

namespace QuestTracker
{
    public partial class MainForm : Form
    {
        public QuestLog questLog;
        public QuestGroup lastSelectedQuestGroup;
        public Quest lastSelectedQuest;

        private float splitRatio;
        private readonly Thread autoSaveThread;

        public MainForm()
        {
            InitializeComponent();

            autoSaveThread = new Thread(AutoSave);

            autoSaveThread.Start();
        }

        private void addQuest_Click(object sender, EventArgs e)
        {
            if (questLog.Groups.Count == 0)
            {
                questLog.Groups.Add(new QuestGroup());
                lastSelectedQuestGroup = questLog.Groups[0];
            }

            if (lastSelectedQuestGroup == null)
                questLog.Groups[0].Quests.Add(new Quest());
            else
                lastSelectedQuestGroup.Quests.Add(new Quest());

            RenderLog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            splitRatio = (float)quests.Width / Width;
            questLog = FileWriter.ReadFromFile();
            RenderLog();
        }

        private void RenderLog()
        {
            quests.Controls.Clear();

            foreach(QuestGroup questGroup in questLog.Groups)
            {
                var questGroupControl = new QuestGroupControl {Dock = DockStyle.Top, QuestGroup = questGroup};

                if (!showCompleted.Checked)
                foreach (Control questControl in questGroupControl.Controls)
                {
                    if (questControl.GetType() == typeof(QuestControl))
                        if (((QuestControl)questControl).Quest.Completed)
                        {
                            questControl.Visible = false;
                            questGroupControl.Height -= 24;
                        }
                }

                quests.Controls.Add(questGroupControl);

                questGroupControl.BringToFront();
            }
        }

        private void addGroup_Click(object sender, EventArgs e)
        {
            questLog.Groups.Add(new QuestGroup());

            RenderLog();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            delete.Left = Width - 130;
            uncomplete.Left = Width - 258;

            var newWidth = (int)(Width * splitRatio);
            newWidth = Width - newWidth < 176 ? Width - 176 : newWidth;
            newWidth = newWidth < 150 ? 150 : newWidth;
            quests.Width = newWidth;
        }

        private void splitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitRatio = (float)quests.Width / Width;
        }

        private void complete_Click(object sender, EventArgs e)
        {
            foreach (Control questGroup in quests.Controls)
                foreach (Control questControl in questGroup.Controls)
                    if (questControl.GetType() == typeof(QuestControl))
                    {
                        if (((QuestControl)questControl).selected.Checked)
                        {
                            Quest quest = ((QuestControl)questControl).Quest;
                            quest.Completed = true;

                            if (quest.CompleteDate == DateTime.MinValue)
                                quest.CompleteDate = DateTime.Now;

                            ((QuestControl)questControl).selected.Checked = false;
                        }
                    }

            foreach (Control questGroup in quests.Controls)
                foreach (Control questControl in questGroup.Controls)
                    if (questControl.GetType() == typeof(QuestControl))
                        if (((QuestControl)questControl).Quest == lastSelectedQuest)
                            questControl.Focus();

            RenderLog();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            foreach (Control questGroup in quests.Controls)
            {
                foreach (Control questControl in questGroup.Controls)
                {
                    if (questControl.GetType() == typeof (QuestControl))
                    {
                        if (((QuestControl)questControl).selected.Checked)
                        {
                            ((QuestGroupControl)questGroup).QuestGroup.Quests.Remove(((QuestControl)questControl).Quest);
                        }
                    }
                }

                if (questGroup.GetType() == typeof (QuestGroupControl))
                {
                    if (((QuestGroupControl)questGroup).selected.Checked)
                        questLog.Groups.Remove(((QuestGroupControl)questGroup).QuestGroup);
                }
            }

            RenderLog();
        }

        private void uncomplete_Click(object sender, EventArgs e)
        {
            foreach (Control questGroup in quests.Controls)
                foreach (Control questControl in questGroup.Controls)
                    if (questControl.GetType() == typeof(QuestControl))
                    {
                        if (((QuestControl)questControl).selected.Checked)
                        {
                            Quest quest = ((QuestControl)questControl).Quest;
                            quest.Completed = false;
                            quest.CompleteDate = DateTime.MinValue;
                            ((QuestControl)questControl).selected.Checked = false;
                        }
                    }

            foreach (Control questGroup in quests.Controls)
                foreach (Control questControl in questGroup.Controls)
                    if (questControl.GetType() == typeof(QuestControl))
                        if (((QuestControl)questControl).Quest == lastSelectedQuest)
                            questControl.Focus();

            RenderLog();
        }

        private void showCompleted_CheckedChanged(object sender, EventArgs e)
        {
            RenderLog();
        }

        private void AutoSave()
        {
            while (true)
            {
                Thread.Sleep(15000);

                FileWriter.WriteToFile(questLog);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            autoSaveThread.Abort();
            FileWriter.WriteToFile(questLog);
        }

        private void questDescription_TextChanged(object sender, EventArgs e)
        {
            if (questDescription.Focused)
                if (lastSelectedQuest != null)
                    lastSelectedQuest.Description = questDescription.Text;
        }
    }
}
