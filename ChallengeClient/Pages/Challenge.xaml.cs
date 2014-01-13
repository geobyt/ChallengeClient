using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ChallengeClient.ViewModels;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;

namespace ChallengeClient.Pages
{
    public partial class Challenge : PhoneApplicationPage
    {
        private string questId = string.Empty;

        public Challenge()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.InitializeEntityDataContext();
        }

        protected void InitializeEntityDataContext()
        {
            //challenge id
            this.NavigationContext.QueryString.TryGetValue("id", out questId);

            ChallengeViewModel c = App.ViewModel.AvailableItems.FirstOrDefault(x => x.Quest.Id.Equals(questId));

            if (c != null)
            {
                this.DataContext = c;
                PlotLocations(c);
            }
        }

        //plot location of challenge and user's relative position
        private async void PlotLocations(ChallengeViewModel c)
        {
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;

            GeoCoordinate currentLocation = new GeoCoordinate
            (
                myGeocoordinate.Latitude,
                myGeocoordinate.Longitude,
                myGeocoordinate.Altitude ?? Double.NaN,
                myGeocoordinate.Accuracy,
                myGeocoordinate.AltitudeAccuracy ?? Double.NaN,
                myGeocoordinate.Speed ?? Double.NaN,
                myGeocoordinate.Heading ?? Double.NaN
            );

            GeoCoordinate challengeLocation = c.Quest.Location == null ? null : new GeoCoordinate
            (
                c.Quest.Location.clat,
                c.Quest.Location.clong
            );

            ShowLocations(currentLocation, challengeLocation);
            sampleMap.Center = GetMidPoint(currentLocation, challengeLocation);

            var bounds = new LocationRectangle(
                currentLocation.Latitude > challengeLocation.Latitude ? currentLocation.Latitude : challengeLocation.Latitude,
                currentLocation.Longitude < challengeLocation.Longitude ? currentLocation.Longitude : challengeLocation.Longitude,
                currentLocation.Latitude < challengeLocation.Latitude ? currentLocation.Latitude : challengeLocation.Latitude,
                currentLocation.Longitude > challengeLocation.Longitude ? currentLocation.Longitude : challengeLocation.Longitude
            );

            sampleMap.SetView(bounds);
        }

        private void ShowLocations(GeoCoordinate currentLocation, GeoCoordinate challengeLocation)
        {
            // Create a small circle to mark the current location.
            Ellipse currLocationCircle = new Ellipse();
            currLocationCircle.Fill = new SolidColorBrush(Colors.Blue);
            currLocationCircle.Height = 20;
            currLocationCircle.Width = 20;
            currLocationCircle.Opacity = 50;

            Ellipse challengeLocationCircle = new Ellipse();
            challengeLocationCircle.Fill = new SolidColorBrush(Colors.Red);
            challengeLocationCircle.Height = 20;
            challengeLocationCircle.Width = 20;
            challengeLocationCircle.Opacity = 50;

            // Create a MapOverlay to contain the points
            MapOverlay currentOverlay = new MapOverlay();
            currentOverlay.Content = currLocationCircle;
            currentOverlay.PositionOrigin = new Point(0.5, 0.5);
            currentOverlay.GeoCoordinate = currentLocation;

            MapOverlay destOverlay = new MapOverlay();
            destOverlay.Content = challengeLocationCircle;
            destOverlay.PositionOrigin = new Point(0.5, 0.5);
            destOverlay.GeoCoordinate = challengeLocation;

            // Create a MapLayer to contain the MapOverlay.
            MapLayer locationLayer = new MapLayer();
            locationLayer.Add(currentOverlay);
            locationLayer.Add(destOverlay);

            // Add the MapLayer to the Map.
            sampleMap.Layers.Add(locationLayer);
        }

        private GeoCoordinate GetMidPoint(GeoCoordinate point1, GeoCoordinate point2)
        {
            return new GeoCoordinate((point1.Latitude + point2.Latitude) / 2, (point1.Longitude + point2.Longitude) / 2);
        }
    }
}