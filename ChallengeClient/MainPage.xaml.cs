using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using ChallengeClient.ViewModels;

namespace ChallengeClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as MainViewModel;

            if (vm != null)
            {
                ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(MainMap);
                var obj = children.FirstOrDefault(x => x.GetType() == typeof(MapItemsControl)) as MapItemsControl;

                obj.ItemsSource = vm.MapItems;
                //myMap.SetView(new GeoCoordinate(47.6050338745117, -122.334243774414), 16);
            }
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var vm = this.DataContext as MainViewModel;

            if (vm != null && !vm.IsDataLoaded)
            {
                vm.LoadData();
            }
        }

        private void AddNewChallenge_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/CreateChallenge.xaml", UriKind.Relative));
        }
    }
}