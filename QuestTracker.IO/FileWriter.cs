using System;
using System.IO;
using System.Xml.Serialization;
using QuestTracker.Data;

namespace QuestTracker.IO
{
    public static class FileWriter
    {
        public static QuestLog ReadFromFile()
        {
            var path = Settings.GetPath();

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
            var defaultQuestLog = new QuestLog { Version = "0.3" };

            defaultQuestLog.Tabs.Add(new QuestTab {Name = "Default Quests"});

            defaultQuestLog.Tabs[0].Groups.Add(new QuestGroup { Name = "QuestTracker Orientation" });
            defaultQuestLog.Tabs[0].Groups[0].Quests.Add(new Quest { Name = "About QuestTracker", Description = "QuestTracker is a todo list presented in the format of a quest tracking list such as the one in World of Warcraft. Since that tracker is so good at keeping track of the details of the things you need to do in game, I thought I would make an application that you could apply to your real life tasks!\n\nCheck out the other quests for more information about QuestTracker!", StartDate = DateTime.Now });
            defaultQuestLog.Tabs[0].Groups[0].Quests.Add(new Quest { Name = "Editing Quests", Description = "Clicking the \"Add Quest\" button will add a quest to the group that it\'s in. Clicking the \"Add Group\" button will add a new group.\n\nGroups and Quests can be renamed by double clicking their name, and the description can be edited in this very box. QuestTracker will automatically save your log every 15 seconds, and when you exit the program. There's no undo yet, so be wary when making changes! However, there is an import/export feature, and backups are automatically made when deletes are performed.\n\nQuests can be dragged and dropped to reorder them, or to change their groups.\n\nAlso, keep in mind that if you select a group, it will select all items in that group whether you can see them or not. By default, completed quests are hidden, but can be shown by checking the box above this description.", StartDate = DateTime.Now });
            defaultQuestLog.Tabs[0].Groups[0].Quests.Add(new Quest { Name = "Recurring Quests", Description = "Recurring quests are quests that you need to do again, sometime after you've completed them already. An example of this would be a daily chore such as cleaning. To set a quest to recur, simply right-click on the quest and select \"Make Quest Recurring...\" A new window will then appear, allowing you to set the options for recurring your quest.\n\nQuests recurred in this way continually repeat from the start date and time given.\n\nFor example, if you had a quest that recurred every 7 days, and you finished on the 4th, 11th, or 18th day after setting the quest, it will recur 3 days after you completed it. So if you set a quest to recur on a Monday every 7 days, it will always recur again on a Monday, regardless of when you actually complete the quest each week.", StartDate = DateTime.Now });
            defaultQuestLog.Tabs[0].Groups.Add(new QuestGroup { Name = "Nonspecific Quests" });

            return defaultQuestLog;
        }

        public static void WriteToFile(QuestLog questLog)
        {
            var path = Settings.GetPath();

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            Export(questLog, path + "QuestTracker.xml");
        }

        public static void Export(QuestLog questLog)
        {
            var now = DateTime.Now;
            Export(questLog, Settings.GetPath() + "QuestTracker." + now.Month.ToString("00") + "-" + now.Day.ToString("00") + "-" + now.Year.ToString("0000") + "-" + now.Hour.ToString("00") + now.Minute.ToString("00") + now.Second.ToString("00") + ".xml");
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

                if (questLog.Version != "0.5")
                    questLog = FileConverter.ConvertToLatestVersion(filename);

                return questLog;
            }
        }
    }
}
