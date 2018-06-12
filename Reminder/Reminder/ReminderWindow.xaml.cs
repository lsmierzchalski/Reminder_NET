﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for ReminderWindow.xaml
    /// </summary>
    public partial class ReminderWindow : Window
    {
        public ReminderWindow(string content)
        {
            LanguageDictionary ld = new LanguageDictionary();
            Resources.MergedDictionaries.Add(ld.SetLanguageDictionary());

            InitializeComponent();

            textBlockContent.Text = content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
