using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ChallengeClient.Pages
{
    public partial class SetupPage : PhoneApplicationPage
    {
        public SetupPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.ApplicationBar = new ApplicationBar();
            this.ApplicationBar.Mode = ApplicationBarMode.Default;
            this.ApplicationBar.Opacity = 1.0;
            this.ApplicationBar.IsVisible = true;
            this.ApplicationBar.IsMenuEnabled = true;

            var okayButton = new ApplicationBarIconButton();
            okayButton.IconUri = new Uri("check.png", UriKind.Relative);
            okayButton.Text = "button 1";
            this.ApplicationBar.Buttons.Add(okayButton);
        }
    }
}