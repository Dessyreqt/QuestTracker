using System;

namespace QuestTracker
{
    public class Quest
    {
        public string Name { get; set;}
        public string Description { get; set;}
        public DateTime StartDate { get; set;}
        public DateTime CompleteDate { get; set;}
        public bool Completed { get; set; }

        public Quest()
        {
            Name = "Unspecified Quest";
            Description = "Enter Description Here!";
            StartDate = DateTime.Now;
            CompleteDate = DateTime.MinValue;
            Completed = false;
        }
    }
}
