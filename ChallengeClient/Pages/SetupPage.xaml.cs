﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ChallengeClient.ViewModels;

namespace ChallengeClient.Pages
{
    public partial class SetupPage : PhoneApplicationPage
    {
        public SetupPage()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var vm = this.DataContext as AuthViewModel;
            vm.SavePhoneCommand.Execute(this.PhoneNumberTextBox.Text);
        }
    }
}