using System.Collections.Generic;

namespace QuestTracker
{
    public class QuestLog
    {
        public List<QuestGroup> Groups { get; set; }

        public QuestLog()
        {
            Groups = new List<QuestGroup>();
        }
    }
}
