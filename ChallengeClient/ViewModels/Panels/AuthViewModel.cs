﻿using ChallengeClient.Helpers;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeClient.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private INavigationService navService;

        public AuthViewModel(INavigationService navService)
        {

        }

        public bool IsLoggedIn()
        {
            return false;
        }
    }
}
