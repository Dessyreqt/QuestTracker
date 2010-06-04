using System.Collections.Generic;
using System.Xml.Serialization;
using QuestTracker.Data.Properties;

namespace QuestTracker.Data
{
    public class QuestGroup
    {
        public string Name { get; set; }
        [XmlArrayItem("Quest", typeof(Quest))]
        public List<Quest> Quests { get; set; }
        public bool Collapsed { get; set; }
        
        public QuestGroup()
        {
            Name = Resources.DefaultQuestGroupName;
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
            Name = Resources.DefaultQuestGroupName;
            Quests = new List<Quest_0_2>();
        }
    }

}
