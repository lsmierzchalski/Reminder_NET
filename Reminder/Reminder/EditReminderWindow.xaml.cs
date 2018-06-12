using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for AddReminderWindow.xaml
    /// </summary>
    public partial class EditReminderWindow : Window
    {
        private int id;

        public EditReminderWindow(int id)
        {
            LanguageDictionary ld = new LanguageDictionary();
            Resources.MergedDictionaries.Add(ld.SetLanguageDictionary());

            InitializeComponent();

            this.id = id;

            initInputFields();
        }

        private void initInputFields()
        {
            ObservableCollection<string> listNrHours = new ObservableCollection<string>();
            for (int i = 0; i < 24; i++) listNrHours.Add(i > 9 ? i.ToString() : "0" + i.ToString());
            comboBoxSelectHours.ItemsSource = listNrHours;

            ObservableCollection<string> listNrMinutes = new ObservableCollection<string>();
            for (int i = 0; i < 60; i++) listNrMinutes.Add(i > 9 ? i.ToString() : "0" + i.ToString());
            comboBoxSelectMinutes.ItemsSource = listNrMinutes;
            comboBoxSelectSecunds.ItemsSource = listNrMinutes;

            FileWithReminders f = new FileWithReminders();
            ReminderElement r = f.getReminder(id);

            DateTime date;

            if (DateTime.TryParse(r.time, out date))
            {
                comboBoxSelectHours.SelectedIndex = int.Parse(date.Hour.ToString());
                comboBoxSelectMinutes.SelectedIndex = int.Parse(date.Minute.ToString());
                comboBoxSelectSecunds.SelectedIndex = int.Parse(date.Second.ToString());

                dataPicker.SelectedDate = date.Date;
            }

            textBoxContent.Text = r.content;
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditButton(object sender, RoutedEventArgs e)
        {
            ReminderElement r = new ReminderElement();

            DateTime date;
            if (!DateTime.TryParse(dataPicker.Text, out date) || textBoxContent.Text.Length != 0)
            {
                r.time = date.Date.ToString().Substring(0, 10) + " " + comboBoxSelectHours.Text + ":" + comboBoxSelectMinutes.Text + ":" + comboBoxSelectSecunds.Text;

                r.id = id;

                r.content = textBoxContent.Text;

                FileWithReminders f = new FileWithReminders();
                List<ReminderElement> lr = f.readRemindersFromFile();

                f.editReminderToFile(r);

                this.Close();
            }
            else
            {
                MessageBox.Show("Dane są niepoprawne lub nie zostały wprowadzone.", "Błąd");
            }
        }
    }
}
