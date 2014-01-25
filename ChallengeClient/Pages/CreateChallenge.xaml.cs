using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps;
using Windows.Devices.Geolocation;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using ChallengeClient.ViewModels;
using System.Text;
using System.IO;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using ChallengeClient.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace ChallengeClient.Pages
{
    public partial class CreateChallenge : PhoneApplicationPage
    {
        GeoCoordinate currentLocation = null;
        MapLayer locationLayer = null;

        public CreateChallenge()
        {
            InitializeComponent();

            GetLocation();
        }

        private void ShowLocation()
        {
            // Create a small circle to mark the current location.
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Blue);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;

            // Create a MapOverlay to contain the circle.
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = currentLocation;

            // Create a MapLayer to contain the MapOverlay.
            locationLayer = new MapLayer();
            locationLayer.Add(myLocationOverlay);

            // Add the MapLayer to the Map.
            sampleMap.Layers.Add(locationLayer);
        }

        private async void GetLocation()
        {
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            currentLocation = new GeoCoordinate
            (
                myGeocoordinate.Latitude,
                myGeocoordinate.Longitude,
                myGeocoordinate.Altitude ?? Double.NaN,
                myGeocoordinate.Accuracy,
                myGeocoordinate.AltitudeAccuracy ?? Double.NaN,
                myGeocoordinate.Speed ?? Double.NaN,
                myGeocoordinate.Heading ?? Double.NaN
            );

            ShowLocation();
            sampleMap.Center = currentLocation;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var vm  = ServiceLocator.Current.GetInstance<MainViewModel>();
            await ServiceHelpers.DoPostRequest(this.CreatePostContent());

            vm.IsDataLoaded = false;

            NavigationService.GoBack();            
        }

        private string CreatePostContent()
        {
            JObject message = new JObject(
                new JProperty("from", "2404019300"),
                new JProperty("to", HttpUtility.UrlEncode(TextAddress.Text)),
                new JProperty("message", HttpUtility.UrlEncode(TextMessage.Text)),
                new JProperty("lat", currentLocation.Latitude),
                new JProperty("long", currentLocation.Longitude)
            );

            return message.ToString(Newtonsoft.Json.Formatting.None, null);
        }
    }
}