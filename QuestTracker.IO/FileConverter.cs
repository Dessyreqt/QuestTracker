using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using QuestTracker.Data;
using QuestTracker.IO.Properties;

namespace QuestTracker.IO
{
    public static class FileConverter
    {
        public static QuestLog ConvertToLatestVersion(string filename)
        {
            if (!Directory.GetParent(filename).Exists)
                Directory.CreateDirectory(filename);

            using (Stream reader = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new XmlSerializer(typeof(VersionReader));
                var version = (VersionReader)serializer.Deserialize(reader);
                reader.Close();

                //a fall-through switch statement would be handy here.
                if (version.Version == null)
                {
                    var questLog_0_3 = ConvertFile0_2to0_3(filename);
                    var questLog = ConvertQuestLog0_3to0_5(questLog_0_3);
                    return questLog;
                }

                if (version.Version == "0.3")
                {
                    var questLog = ConvertFile0_3to0_5(filename);
                    return questLog;
                }

                //our conversion has failed at this point
                throw new ApplicationException("File conversion failed.");
            }
        }

        public static QuestLog ConvertFile0_3to0_5(string filename)
        {
            if (!Directory.GetParent(filename).Exists)
                Directory.CreateDirectory(filename);

            using (Stream reader = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new XmlSerializer(typeof(QuestLog_0_3));
                var oldQuestLog = (QuestLog_0_3)serializer.Deserialize(reader);
                reader.Close();

                var retVal = ConvertQuestLog0_3to0_5(oldQuestLog);
                FileWriter.Export(retVal, filename.Insert(filename.LastIndexOf('.'), ".to0.5"));
                return retVal;
            }
        }

        private static QuestLog ConvertQuestLog0_3to0_5(QuestLog_0_3 oldQuestLog)
        {
            var retVal = new QuestLog { Version = "0.5", ShowCompletedQuests = oldQuestLog.ShowCompletedQuests };

            retVal.Tabs.Add(new QuestTab { Name = "Primary Quests" });

            foreach (var oldGroup in oldQuestLog.Groups)
            {
                retVal.Tabs[0].Groups.Add(ConvertQuestGroup0_3to0_5(oldGroup));
            }

            return retVal;
        }

        private static QuestGroup ConvertQuestGroup0_3to0_5(QuestGroup_0_3 oldGroup)
        {
            var retVal = new QuestGroup { Collapsed = oldGroup.Collapsed, Name = oldGroup.Name };

            foreach (var oldQuest in oldGroup.Quests)
            {
                retVal.Quests.Add(ConvertQuest0_3to0_5(oldQuest));
            }

            return retVal;
        }

        private static Quest ConvertQuest0_3to0_5(Quest_0_3 oldQuest)
        {
            var retVal = new Quest { Completed = oldQuest.Completed, Description = oldQuest.Description, Name = oldQuest.Name, StartDate = oldQuest.StartDate, CompleteDates = oldQuest.CompleteDates};

            return retVal;
        }


        public static QuestLog_0_3 ConvertFile0_2to0_3(string filename)
        {
            if (!Directory.GetParent(filename).Exists)
                Directory.CreateDirectory(filename);

            using (Stream reader = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new XmlSerializer(typeof (QuestLog_0_2));
                var oldQuestLog = (QuestLog_0_2)serializer.Deserialize(reader);
                reader.Close();

                var retVal = ConvertQuestLog0_2to0_3(oldQuestLog);
                return retVal;
            }
        }

        private static QuestLog_0_3 ConvertQuestLog0_2to0_3(QuestLog_0_2 oldQuestLog)
        {
            var retVal = new QuestLog_0_3 {Version = "0.3", ShowCompletedQuests = oldQuestLog.ShowCompletedQuests};

            foreach (var oldGroup in oldQuestLog.Groups)
            {
                retVal.Groups.Add(ConvertQuestGroup0_2to0_3(oldGroup));
            }

            return retVal;
        }

        private static QuestGroup_0_3 ConvertQuestGroup0_2to0_3(QuestGroup_0_2 oldGroup)
        {
            var retVal = new QuestGroup_0_3 {Collapsed = oldGroup.collapsed, Name = oldGroup.Name};

            foreach(var oldQuest in oldGroup.Quests)
            {
                retVal.Quests.Add(ConvertQuest0_2to0_3(oldQuest));
            }

            return retVal;
        }

        private static Quest_0_3 ConvertQuest0_2to0_3(Quest_0_2 oldQuest)
        {
            var retVal = new Quest_0_3 {Completed = oldQuest.Completed, Description = oldQuest.Description, Name = oldQuest.Name, StartDate = oldQuest.StartDate};

            if (oldQuest.Completed)
                retVal.CompleteDates.Add(oldQuest.CompleteDate);

            return retVal;
        }
    }

    [XmlRoot("QuestLog")]
    public class VersionReader
    {
        public string Version { get; set; }
    }
}

namespace QuestTracker.Data
{
    [XmlRoot("QuestLog")]
    public class QuestLog_0_3
    {
        public string Version { get; set; }
        [XmlArrayItem("QuestGroup", typeof(QuestGroup_0_3))]
        public List<QuestGroup_0_3> Groups { get; set; }
        public bool ShowCompletedQuests { get; set; }

        public QuestLog_0_3()
        {
            Groups = new List<QuestGroup_0_3>();
            ShowCompletedQuests = false;
        }
    }

    public class QuestGroup_0_3
    {
        public string Name { get; set; }
        [XmlArrayItem("Quest", typeof(Quest_0_3))]
        public List<Quest_0_3> Quests { get; set; }
        public bool Collapsed { get; set; }

        public QuestGroup_0_3()
        {
            Name = Resources.DefaultQuestGroupName;
            Quests = new List<Quest_0_3>();
        }
    }


    public class Quest_0_3
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public List<DateTime> CompleteDates { get; set; }
        public bool Completed { get; set; }
        public bool Recurring { get; set; }
        public RecurrenceSchedule Schedule { get; set; }

        public Quest_0_3()
        {
            Name = Resources.DefaultQuestName;
            Description = Resources.DefaultQuestDescription;
            StartDate = DateTime.Now;
            CompleteDates = new List<DateTime>();
            Completed = false;
            Recurring = false;
            Schedule = new RecurrenceSchedule();
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

    public class Quest_0_2
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public bool Completed { get; set; }

        public Quest_0_2()
        {
            Name = Resources.DefaultQuestName;
            Description = Resources.DefaultQuestDescription;
            StartDate = DateTime.Now;
            CompleteDate = DateTime.MinValue;
            Completed = false;
        }
    }
}