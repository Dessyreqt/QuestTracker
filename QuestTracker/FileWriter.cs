using System;
using System.IO;
using System.Xml.Serialization;

namespace QuestTracker
{
    public static class FileWriter
    {
        public static QuestLog ReadFromFile()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\QuestTracker\\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            try
            {
                return Import(path + "QuestTracker.xml");
            }
            catch (Exception)
            {
                return DefaultQuestLog();
            }
        }

        private static QuestLog DefaultQuestLog()
        {
            var defaultQuestLog = new QuestLog();

            defaultQuestLog.Version = "0.3";
            defaultQuestLog.Groups.Add(new QuestGroup {Name = "QuestTracker Orientation"});
            defaultQuestLog.Groups[0].Quests.Add(new Quest { Name = "About QuestTracker", Description = "QuestTracker is a todo list presented in the format of a quest tracking list such as the one in World of Warcraft. Since that tracker is so good at keeping track of the details of the things you need to do in game, I thought I would make an application that you could apply to your real life tasks!\n\nCheck out the other quests for more information about QuestTracker!", StartDate = DateTime.Now });
            defaultQuestLog.Groups[0].Quests.Add(new Quest { Name = "Editing Quests", Description = "Clicking the \"Add Quest\" button will add a quest to the group that it\'s in. Clicking the \"Add Group\" button will add a new group.\n\nGroups and Quests can be renamed by double clicking their name, and the description can be edited in this very box. QuestTracker will automatically save your log every 15 seconds, and when you exit the program. There's no undo yet, so be wary when making changes! However, there is an import/export feature, and backups are automatically made when deletes are performed.\n\nAs of yet, there is no functionality to change the order of the quests in your log.\n\nAlso, keep in mind that if you select a group, it will select all items in that group whether you can see them or not. By default, completed quests are hidden, but can be shown by checking the box above this description.", StartDate = DateTime.Now });
            defaultQuestLog.Groups.Add(new QuestGroup { Name = "Nonspecific Quests" });

            return defaultQuestLog;
        }

        public static void WriteToFile(QuestLog questLog)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\QuestTracker\\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            Export(questLog, path + "QuestTracker.xml");
        }

        public static void Export(QuestLog questLog)
        {
            var now = DateTime.Now;
            Export(questLog, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\QuestTracker\\" + "QuestTracker." + now.Month.ToString("00") + "-" + now.Day.ToString("00") + "-" + now.Year.ToString("0000") + "-" + now.Hour.ToString("00") + now.Minute.ToString("00") + now.Second.ToString("00") + ".xml");
        }

        public static void Export(QuestLog questLog, string filename)
        {
            if (!Directory.GetParent(filename).Exists)
                Directory.CreateDirectory(filename);

            using (Stream writer = new FileStream(filename, FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(QuestLog));
                serializer.Serialize(writer, questLog);
                writer.Close();
            }
        }

        public static QuestLog Import(string filename)
        {

            if (!Directory.GetParent(filename).Exists)
                Directory.CreateDirectory(filename);

            using (Stream reader = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new XmlSerializer(typeof(QuestLog));
                var questLog = (QuestLog)serializer.Deserialize(reader);
                reader.Close();

                if (questLog.Version == null)
                    questLog = FileConverter.ConvertFrom0_2(filename);
                else if (questLog.Version != "0.3")
                    throw new Exception("Unrecognized file version. This version of QuestTracker accepts files in the 0.3 format and earlier.");

                return questLog;
            }
        }
    }
}
