using System.Windows;
using Microsoft.Phone.Maps.Controls;

namespace ChallengeClient.Controls
{
    public class MapViewDependencyProperty : DependencyObject
    {
        public LocationRectangle View
        {
            get {
                return (LocationRectangle)GetValue(ViewProperty);
            }
            set { 
                SetValue(ViewProperty, value); }
        }

        public static readonly DependencyProperty ViewProperty =
            DependencyProperty.Register("View", typeof(LocationRectangle), typeof(Map), new PropertyMetadata(null, new PropertyChangedCallback(onViewChanged)));

        
        private static void onViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var map = d as Map;
            if (map != null)
                map.SetView(e.NewValue as LocationRectangle);
        }

        public static void SetView(UIElement element, LocationRectangle value)
        {

        }

        public static string GetView(UIElement element)
        {
            return null;
        }  
    }
}


