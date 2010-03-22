using System.Collections.Generic;

namespace QuestTracker
{
    public class QuestGroup
    {
        public string Name { get; set; }
        public List<Quest> Quests { get; set; }
        public bool collapsed;
        
        public QuestGroup()
        {
            Name = "Nonspecific Quests";
            Quests = new List<Quest>();
        }
    }
}
