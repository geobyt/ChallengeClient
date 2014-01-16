using ChallengeClient.Helpers;
using ChallengeClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeClient.ViewModels
{
    public class MapPinViewModel : ViewModelBase
    {
        private MapPin pin;
        public MapPin Pin
        {
            get { return this.pin; }
            set { this.pin = value; }
        }

        public RelayCommand Navigate
        {
            get;
            private set;
        }

        public MapPinViewModel(MapPin pin)
        {
            this.Pin = pin;
            this.Navigate = new RelayCommand(NavigateToQuest);
            this.Navigate.IsEnabled = true;
        }

        private void NavigateToQuest()
        {
            App.RootFrame.Navigate(new Uri(string.Format("/Pages/Challenge.xaml?id={0}", this.Pin.Id), UriKind.Relative));
        }
    }
}
