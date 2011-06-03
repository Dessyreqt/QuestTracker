using System;
using System.Collections.Generic;
using QuestTracker.Data.Properties;

namespace QuestTracker.Data
{
    public class Quest
    {
        public string Name { get; set;}
        public string Description { get; set;}
        public DateTime StartDate { get; set;}
        public List<DateTime> CompleteDates { get; set;}
        public bool Completed { get; set; }
        public bool Recurring { get; set; }
        public RecurrenceSchedule Schedule { get; set; }
       
        public Quest()
        {
            Name = Resources.DefaultQuestName;
            Description = Resources.DefaultQuestDescription;
            StartDate = DateTime.Now;
            CompleteDates = new List<DateTime>();
            Completed = false;
            Recurring = false;
            Schedule = new RecurrenceSchedule();
        }
    }
}
