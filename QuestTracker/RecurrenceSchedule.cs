using System;

namespace QuestTracker
{
    public enum RecurrenceUnit
    {
        Minutes = 0,
        Hours = 1,
        Days = 2
    }

    public class RecurrenceSchedule
    {
        public DateTime StartDate { get; set; }
        public int Frequency { get; set; }
        public RecurrenceUnit Unit { get; set; }

        public RecurrenceSchedule()
        {
            StartDate = DateTime.MinValue;
            Frequency = 0;
            Unit = RecurrenceUnit.Minutes;
        }
    }
}
