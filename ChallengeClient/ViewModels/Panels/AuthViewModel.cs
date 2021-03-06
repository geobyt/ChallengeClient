﻿using ChallengeClient.Helpers;
using ChallengeClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ChallengeClient.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private INavigationService navService;

        private const string PHONE_NUMBER_SETTINGS = "phoneNumber";

        private const string AUTH_TOKEN_SETTINGS = "authToken";

        private IsolatedStorageSettings localSettings;

        #region Phone Number

        private string phoneNumber;

        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.Set("PhoneNumber", ref this.phoneNumber, value); }
        }

        #endregion

        #region Validation Code

        private string validationCode;

        public string ValidationCode
        {
            get { return validationCode; }
            set { this.Set("ValidationCode", ref this.validationCode, value); }
        }

        #endregion

        public RelayCommand<string> SavePhoneCommand
        {
            get;
            private set; 
        }

        public RelayCommand<string> ValidateCommand
        {
            get;
            private set;
        }

        private AuthService authService;


        public AuthViewModel(INavigationService navService, AuthService authService)
        {
            this.localSettings = IsolatedStorageSettings.ApplicationSettings;

            this.SavePhoneCommand = new RelayCommand<string>(this.SavePhone);
            this.ValidateCommand = new RelayCommand<string>(this.Validate);
            this.authService = authService;
            this.navService = navService;
        }

        private async void SavePhone(string phoneNumber)
        {
            this.localSettings[PHONE_NUMBER_SETTINGS] = phoneNumber;
            this.localSettings.Save();

            await this.authService.RequestValidationCodeAsync(phoneNumber);
            this.navService.NavigateTo(new Uri("/Pages/VerificationPage.xaml", UriKind.Relative));
        }
        
        private async void Validate(string validationCode)
        {
            var authToken = await this.authService.RequestAuthTokenAsync(validationCode);

            if (authToken != null)
            {
                this.Login(authToken);
                this.navService.NavigateTo(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        public void Login(string authToken)
        {
            this.localSettings[AUTH_TOKEN_SETTINGS] = authToken;
            this.localSettings.Save();
        }

        public bool IsLoggedIn()
        {
            return this.localSettings.Contains(AUTH_TOKEN_SETTINGS);
        }
    }
}
