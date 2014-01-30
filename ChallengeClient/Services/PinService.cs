using ChallengeClient.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeClient.Services
{
    public class PinService
    {
        private string phoneNumber;

        private IsolatedStorageSettings localSettings;

        private string PhoneNumber
        {
            get
            {
                if (this.phoneNumber == null)
                {
                    this.phoneNumber = this.localSettings["phoneNumber"] as string;
                }

                return this.phoneNumber;
            }
        }

        public PinService()
        {
            this.localSettings = IsolatedStorageSettings.ApplicationSettings;
        }

        public async Task<HttpResponseMessage> GetMyPins()
        {
            return await ServiceHelpers.GetAsync(string.Format("/pins/{0}", this.phoneNumber));
        }
    }
}
