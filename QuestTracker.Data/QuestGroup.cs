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
}
