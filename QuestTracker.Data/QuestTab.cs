using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuestTracker.Data
{
    public class QuestTab
    {
        public string Name { get; set; }
        [XmlArrayItem("QuestGroup", typeof(QuestGroup))]
        public List<QuestGroup> Groups { get; set; }

        public QuestTab()
        {
            Groups = new List<QuestGroup>();
        }
    }
}
