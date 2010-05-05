using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuestTracker
{
    public class QuestGroup
    {
        public string Name { get; set; }
        [XmlArrayItem("Quest", typeof(Quest))]
        public List<Quest> Quests { get; set; }
        public bool collapsed;
        
        public QuestGroup()
        {
            Name = "Nonspecific Quests";
            Quests = new List<Quest>();
        }
    }

    public class QuestGroup_0_2
    {
        public string Name { get; set; }
        [XmlArrayItem("Quest", typeof(Quest_0_2))]
        public List<Quest_0_2> Quests { get; set; }
        public bool collapsed;

        public QuestGroup_0_2()
        {
            Name = "Nonspecific Quests";
            Quests = new List<Quest_0_2>();
        }
    }

}
