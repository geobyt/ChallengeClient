/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ChallengeClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using ChallengeClient.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace ChallengeClient.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var container = SimpleIoc.Default;

            var mainFrame = App.Current.RootVisual as PhoneApplicationFrame;

            container.Register<INavigationService, NavigationService>();
            container.Register<AuthViewModel>();
            container.Register<MainViewModel>();
        }

        /// <summary>
        /// Just a shortcut for getting something out of the service locator
        /// </summary>
        /// <typeparam name="T">A type to retrieve from the service locator</typeparam>
        /// <returns>An instance retrieved from instance from service locator</returns>
        private T Get<T>() {
            return ServiceLocator.Current.GetInstance<T>();
        }

        public MainViewModel Main
        {
            get { return this.Get<MainViewModel>(); }
        }

        public AuthViewModel Auth
        {
            get { return this.Get<AuthViewModel>(); }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}