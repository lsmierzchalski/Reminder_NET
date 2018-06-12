using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Reminder
{

    class FileWithReminders
    {

        const string FILE_PATH = "file_with_remainders.json";

        public List<ReminderElement> readRemindersFromFile()
        {
            string path = FILE_PATH;

            List<ReminderElement> listReminders = new List<ReminderElement>();

            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(FILE_PATH))
                    {
                        listReminders = JsonConvert.DeserializeObject<List<ReminderElement>>(sr.ReadToEnd());
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("The file could not be read:" + e.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            } else
            {
                using (FileStream fs = File.Create(path)){}
            }

            //MessageBox.Show("{" + reminderList[0].Time + "}", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);

            return listReminders;
        }

        public void saveReminderToFile(ReminderElement reminder)
        {
            List<ReminderElement> listReminders = readRemindersFromFile();

            listReminders.Add(reminder);

            saveRemindersToFile(listReminders);
        }

        public void saveRemindersToFile(List<ReminderElement> listReminders)
        {
            string path = FILE_PATH;

            string sjson = JsonConvert.SerializeObject(listReminders, Formatting.Indented);

            if (File.Exists(path))
            {
                File.WriteAllText(path, sjson);
            }
        }

        public void deleteReminder(int id)
        {
            List<ReminderElement> listReminders = readRemindersFromFile();

            foreach(ReminderElement r in listReminders)
            {
                if(r.id == id)
                {
                    listReminders.Remove(r);
                    break;
                }
            }

            saveRemindersToFile(listReminders);
        }

        public ReminderElement getReminder(int id)
        {
            List<ReminderElement> listReminders = readRemindersFromFile();

            foreach (ReminderElement r in listReminders)
            {
                if (r.id == id)
                {
                    return r;
                }
            }

            return null;
        }

        public void editReminderToFile(ReminderElement reminder)
        {
            List<ReminderElement> listReminders = readRemindersFromFile();

            foreach (ReminderElement r in listReminders)
            {
                if (r.id == reminder.id)
                {
                    r.time = reminder.time;
                    r.content = reminder.content;
                    break;
                }
            }

            saveRemindersToFile(listReminders);
        }

        private static FileStream Create(string path)
        {
            throw new NotImplementedException();
        }

        private static bool Exists(string path)
        {
            throw new NotImplementedException();
        }
    }
}
