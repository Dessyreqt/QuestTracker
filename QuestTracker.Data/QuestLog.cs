using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuestTracker.Data
{
    [XmlRoot("QuestLog")]
    public class QuestLog
    {
        public string Version { get; set; }
        [XmlArrayItem("QuestTab", typeof(QuestTab))]
        public List<QuestTab> Tabs { get; set; }
        public bool ShowCompletedQuests { get; set; }
        [XmlIgnore]
        public bool Edited { get; set; }

        public QuestLog()
        {
            Tabs = new List<QuestTab>();
            ShowCompletedQuests = false;
        }
    }
}
