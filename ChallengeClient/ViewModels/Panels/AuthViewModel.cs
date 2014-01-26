using ChallengeClient.Helpers;
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

        private const string VALIDATION_CODE_SETTINGS = "validationCode";

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

        public RelayCommand ValidateCommand
        {
            get;
            private set;
        }

        private AuthService authService;


        public AuthViewModel(INavigationService navService, AuthService authService)
        {
            this.localSettings = IsolatedStorageSettings.ApplicationSettings;

            this.SavePhoneCommand = new RelayCommand<string>(this.SavePhone);
            this.ValidateCommand = new RelayCommand(this.Validate);
            this.authService = authService;
            this.navService = navService;
        }

        private async void SavePhone(string phoneNumber)
        {
            this.localSettings[PHONE_NUMBER_SETTINGS] = phoneNumber;
            //await this.authService.RequestValidationCodeAsync(phoneNumber);
            this.navService.NavigateTo(new Uri("/Pages/VerificationPage.xaml", UriKind.Relative));
        }
        
        private async void Validate(string validationCode)
        {
            this.localSettings[PHONE_NUMBER_SETTINGS] = phoneNumber;
        }

        public bool IsLoggedIn()
        {
            return false;
        }
    }
}
