using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ReminderElement> collectionReminders;
        private List<ReminderElement> listReminderElement;

        public MainWindow()
        {
            LanguageDictionary ld = new LanguageDictionary();
            Resources.MergedDictionaries.Add(ld.SetLanguageDictionary());

            InitializeComponent();

            RefreshListReminders();

            Timer();
            ReminderTrigger();
        }

        private void Timer()
        {
            FileWithReminders f = new FileWithReminders();
            List<ReminderElement> lr;

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                textBlockTimer.Text = DateTime.Now.ToString();
            }, Dispatcher);
        }

        private void ReminderTrigger()
        {
            FileWithReminders f = new FileWithReminders();
            List<ReminderElement> lr;

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                lr = f.readRemindersFromFile();
                foreach (ReminderElement r in lr)
                {
                    if (DateTime.Now.ToString() == r.time)
                    {
                        ReminderWindow rw = new ReminderWindow(r.content);
                        rw.Show();
                    }
                }

            }, Dispatcher);
        }

        private void AddReminder(object sender, RoutedEventArgs e)
        {
            AddReminderWindow addReminderWindow = new AddReminderWindow();
            addReminderWindow.ShowDialog();
            RefreshListReminders();
        }

        public void RefreshListReminders()
        {
            FileWithReminders f = new FileWithReminders();
            listReminderElement = f.readRemindersFromFile();
            if (listReminderElement.Count != 0) showListRemainders(listReminderElement);
        }

        private void showListRemainders(List<ReminderElement> listReminders)
        {
            collectionReminders = new ObservableCollection<ReminderElement>();
            foreach (ReminderElement r in listReminders)
            {
                collectionReminders.Add(r);
            }
            listViewReminders.DataContext = collectionReminders;    
        }

        private void deleteReminder_Clicked(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is ReminderElement)
            {
                ReminderElement deleteme = (ReminderElement)cmd.DataContext;
                MessageBoxResult result = MessageBox.Show("Czy napewno chcesz usunąć przypomnienie?\nO treści: " + deleteme.content + "\nZaplanowanym na: " + deleteme.time, "Usuń", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                if (result == MessageBoxResult.Yes) {
                    collectionReminders.Remove(deleteme);

                    FileWithReminders f = new FileWithReminders();
                    f.deleteReminder(deleteme.id);
                }
            }
        }

        private void editReminder_Clicked(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is ReminderElement)
            {
                ReminderElement editme = (ReminderElement)cmd.DataContext;

                EditReminderWindow editReminderWindow = new EditReminderWindow(editme.id);
                editReminderWindow.ShowDialog();
                RefreshListReminders();
            }
        }
    }
}
