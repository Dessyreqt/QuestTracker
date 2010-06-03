using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QuestTracker.Data;
using QuestTracker.QuestControls.Properties;

namespace QuestTracker.QuestControls
{
    public partial class RecurringQuestDialog : Form
    {
        private readonly Quest quest;

        public RecurringQuestDialog(Quest quest)
        {
            InitializeComponent();

            this.quest = quest;
        }

        private void RecurringQuestDialog_Load(object sender, EventArgs e)
        {
            Top = Owner.Top + Owner.Height / 2 - 84;
            Left = Owner.Left + Owner.Width / 2 - 141;

            recur.Checked = quest.Recurring;
            
            startDate.Value = quest.Schedule.StartDate == DateTime.MinValue ? DateTime.Now : quest.Schedule.StartDate;

            frequency.Text = quest.Schedule.Frequency.ToString();
            unit.SelectedIndex = (int)quest.Schedule.Unit;
        }

        private void quantity_TextChanged(object sender, EventArgs e)
        {
            frequency.Text = Regex.Replace(frequency.Text, "[^0-9]", "");
        }

        private void cancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (recur.Checked && int.Parse(frequency.Text) == 0)
            {
                MessageBox.Show(Resources.RecurringQuestFrequencyError, Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            quest.Recurring = recur.Checked;
            quest.Schedule.StartDate = startDate.Value;
            quest.Schedule.Frequency = int.Parse(frequency.Text);
            quest.Schedule.Unit = (RecurrenceUnit)unit.SelectedIndex;

            Close();
        }
    }
}
