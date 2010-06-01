using System;
using System.Collections.Generic;

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
            Name = "Unspecified Quest";
            Description = "Enter Description Here!";
            StartDate = DateTime.Now;
            CompleteDates = new List<DateTime>();
            Completed = false;
            Recurring = false;
            Schedule = new RecurrenceSchedule();
        }
    }

    public class Quest_0_2
    {
        public string Name { get; set;}
        public string Description { get; set;}
        public DateTime StartDate { get; set;}
        public DateTime CompleteDate { get; set;}
        public bool Completed { get; set; }

        public Quest_0_2()
        {
            Name = "Unspecified Quest";
            Description = "Enter Description Here!";
            StartDate = DateTime.Now;
            CompleteDate = DateTime.MinValue;
            Completed = false;
        }
    }
}
