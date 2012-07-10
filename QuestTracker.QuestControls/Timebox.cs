using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuestTracker.Data;

namespace QuestTracker.QuestControls
{
    public partial class Timebox : Form
    {
        private QuestGroup questGroup;
        private Quest currentQuest;
        private DateTime timerEnd;
        private bool onBreak;
        private int taskNum;

        public QuestGroup QuestGroup
        { 
            get { return questGroup; }
            set
            {
                questGroup = value;
                taskNum = 0;
                if (questGroup.Quests.Count > taskNum) 
                    SetCurrentQuest(questGroup.Quests[taskNum]);
            }
        }

        private void SetCurrentQuest(Quest quest)
        {
            currentQuest = quest;
            currentTask.Text = currentQuest.Name;
        }

        public Timebox(QuestGroup questGroup)
        {
            InitializeComponent();
            QuestGroup = questGroup;
        }

        private void startTask_Click(object sender, EventArgs e)
        {
            StartTask();
        }

        private void StartTask()
        {
            timerEnd = DateTime.Now.AddMinutes(int.Parse(taskLength.Text));
            onBreak = false;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeLeft = timerEnd - DateTime.Now;
            if (timeLeft.TotalSeconds > 0)
            {
                timeRemaining.Text = string.Format("{0}:{1:00}", (int)timeLeft.TotalMinutes, timeLeft.Seconds);
            }
            else
            {
                timeRemaining.Text = "Not Running";
                timer.Stop();
                if (onBreak)
                {
                    var breakDialog = new TimeboxBreakDialog();
                    breakDialog.ShowDialog(ParentForm);
                    switch (breakDialog.DialogResult)
                    {
                        case DialogResult.OK:
                            StartTask();
                            break;
                        case DialogResult.Retry:
                            StartBreak();
                            break;
                    }
                }
                else
                {
                    var taskDialog = new TimeboxTaskDialog();
                    taskDialog.ShowDialog(ParentForm);
                    switch (taskDialog.DialogResult)
                    {
                        case DialogResult.OK:
                            StartBreak();
                            break;
                        case DialogResult.Retry:
                            StartTask();
                            break;
                    }
                }
            }
        }

        private void startBreak_Click(object sender, EventArgs e)
        {
            StartBreak();
        }

        private void StartBreak()
        {
            timerEnd = DateTime.Now.AddMinutes(int.Parse(breakLength.Text));
            onBreak = true;
            timer.Start();
        }

        private void newTask_Click(object sender, EventArgs e)
        {
            GetNewTask();
        }

        private void GetNewTask()
        {
            taskNum++;
            if (taskNum >= questGroup.Quests.Count)
                taskNum = 0;
            SetCurrentQuest(questGroup.Quests[taskNum]);
        }
    }
}
