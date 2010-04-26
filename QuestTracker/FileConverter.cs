using System.IO;
using System.Xml.Serialization;

namespace QuestTracker
{
    public static class FileConverter
    {
        public static QuestLog ConvertFrom0_2(string filename)
        {
            if (!Directory.GetParent(filename).Exists)
                Directory.CreateDirectory(filename);

            using (Stream reader = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new XmlSerializer(typeof (QuestLog_0_2));
                var oldQuestLog = (QuestLog_0_2)serializer.Deserialize(reader);
                reader.Close();

                var retVal = ConvertQuestLog0_2to0_3(oldQuestLog);
                FileWriter.Export(retVal, filename.Insert(filename.LastIndexOf('.'), ".to0.3"));
                return retVal;
            }
        }

        private static QuestLog ConvertQuestLog0_2to0_3(QuestLog_0_2 oldQuestLog)
        {
            var retVal = new QuestLog();

            retVal.Version = "0.3";
            retVal.ShowCompletedQuests = oldQuestLog.ShowCompletedQuests;

            foreach (var oldGroup in oldQuestLog.Groups)
            {
                retVal.Groups.Add(ConverQuestGroup0_2to0_3(oldGroup));
            }

            return retVal;
        }

        private static QuestGroup ConverQuestGroup0_2to0_3(QuestGroup_0_2 oldGroup)
        {
            var retVal = new QuestGroup();

            retVal.collapsed = oldGroup.collapsed;
            retVal.Name = oldGroup.Name;
            foreach(var oldQuest in oldGroup.Quests)
            {
                retVal.Quests.Add(ConvertQuest0_2to0_3(oldQuest));
            }

            return retVal;
        }

        private static Quest ConvertQuest0_2to0_3(Quest_0_2 oldQuest)
        {
            var retVal = new Quest();

            retVal.Completed = oldQuest.Completed;
            retVal.Description = oldQuest.Description;
            retVal.Name = oldQuest.Name;
            retVal.StartDate = oldQuest.StartDate;
            if (oldQuest.Completed)
                retVal.CompleteDates.Add(oldQuest.CompleteDate);

            return retVal;
        }
    }
}
