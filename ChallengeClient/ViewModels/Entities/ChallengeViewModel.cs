using ChallengeClient.Helpers;
using ChallengeClient.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChallengeClient.ViewModels
{
    public class ChallengeViewModel : ViewModelBase
    {
        private Challenge quest;
        public Challenge Quest
        {
            get { return this.quest; }
            set { this.quest = value; }
        }

        public RelayCommand Navigate
        {
            get;
            private set;
        }

        public ChallengeViewModel(Challenge quest)
        {
            this.Quest = quest;
            this.Navigate = new RelayCommand(NavigateToQuest);            
        }

        private void NavigateToQuest()
        {
            App.RootFrame.Navigate(new Uri(string.Format("/Pages/Challenge.xaml?id={0}", this.Quest.Id), UriKind.Relative));
        }
    }
}
