using Microsoft.Phone.Controls;
using System;
using System.Windows.Navigation;

namespace ChallengeClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void AddNewChallenge_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/CreateChallenge.xaml", UriKind.Relative));
        }
    }
}