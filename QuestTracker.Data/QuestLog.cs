using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuestTracker.Data
{
    [XmlRoot("QuestLog")]
    public class QuestLog
    {
        public string Version { get; set; }
        [XmlArrayItem("QuestGroup", typeof(QuestGroup))]
        public List<QuestGroup> Groups { get; set; }
        public bool ShowCompletedQuests { get; set; }

        public QuestLog()
        {
            Groups = new List<QuestGroup>();
            ShowCompletedQuests = false;
        }
    }

    [XmlRoot("QuestLog")]
    public class QuestLog_0_2
    {
        [XmlArrayItem("QuestGroup", typeof(QuestGroup_0_2))]
        public List<QuestGroup_0_2> Groups { get; set; }
        public bool ShowCompletedQuests { get; set; }

        public QuestLog_0_2()
        {
            Groups = new List<QuestGroup_0_2>();
            ShowCompletedQuests = false;
        }
    }

}
