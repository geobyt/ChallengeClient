using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
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

            Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(MainMap);
            var obj = children.FirstOrDefault(x => x.GetType() == typeof(MapItemsControl)) as MapItemsControl;

            obj.ItemsSource = App.ViewModel.AvailableItems;
            //myMap.SetView(new GeoCoordinate(47.6050338745117, -122.334243774414), 16);
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