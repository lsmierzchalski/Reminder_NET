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
    public partial class AddReminderWindow : Window
    {

        public AddReminderWindow()
        {
            LanguageDictionary ld = new LanguageDictionary();
            Resources.MergedDictionaries.Add(ld.SetLanguageDictionary());

            InitializeComponent();

            initInputFields();
        }

        private void initInputFields()
        {
            dataPicker.SelectedDate = DateTime.Now.Date;

            ObservableCollection<string> listNrHours = new ObservableCollection<string>();
            for (int i = 0; i < 24; i++) listNrHours.Add( i > 9 ?i.ToString() : "0" + i.ToString());
            comboBoxSelectHours.ItemsSource = listNrHours;
            comboBoxSelectHours.SelectedIndex = 0;

            ObservableCollection<string> listNrMinutes = new ObservableCollection<string>();
            for (int i = 0; i < 60; i++) listNrMinutes.Add(i > 9 ? i.ToString() : "0" + i.ToString());
            comboBoxSelectMinutes.ItemsSource = listNrMinutes;
            comboBoxSelectSecunds.ItemsSource = listNrMinutes;
            comboBoxSelectMinutes.SelectedIndex = 0;
            comboBoxSelectSecunds.SelectedIndex = 0;
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            ReminderElement r = new ReminderElement();

            DateTime date;
            if (!DateTime.TryParse(dataPicker.Text,out date) || textBoxContent.Text.Length != 0)
            {
                r.time = date.Date.ToString().Substring(0, 10) + " " + comboBoxSelectHours.Text + ":" + comboBoxSelectMinutes.Text + ":" + comboBoxSelectSecunds.Text;

                FileWithReminders f = new FileWithReminders();
                List<ReminderElement> lr = f.readRemindersFromFile();

                if (lr.Count != 0) r.id = lr[lr.Count() - 1].id + 1;
                else r.id = 0;

                r.content = textBoxContent.Text;

                lr.Add(r);
                f.saveRemindersToFile(lr);

                this.Close();
            } else
            {
                MessageBox.Show("Dane są niepoprawne lub nie zostały wprowadzone.", "Błąd");
            }
        }
    }
}
