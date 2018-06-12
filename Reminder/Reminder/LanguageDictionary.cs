using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Reminder
{
    class LanguageDictionary
    {
        //funkcja rozpoznaje język systemu i na tej podstawie przypiuje plik z którego będą pobierane wartości tzw. DynamicResource
        //https://stackoverflow.com/questions/11327840/multilingual-wpf-application
        public ResourceDictionary SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en":
                    dict.Source = new Uri("..\\Resources\\StringResources.xaml", UriKind.Relative);
                    break;
                case "pl-PL":
                    dict.Source = new Uri("..\\Resources\\StringResources.pl-PL.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\StringResources.xaml", UriKind.Relative);
                    break;
            }
            return dict;
        }
    }
}
