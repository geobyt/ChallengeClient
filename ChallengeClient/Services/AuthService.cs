using ChallengeClient.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeClient.Services
{
    public class AuthService
    {
        public async Task RequestValidationCodeAsync(string phoneNumber)
        {
            var response = await ServiceHelpers.GetAsync(string.Format("/validation_code/{0}", phoneNumber));
            var body = await response.Content.ReadAsStringAsync();
            JObject jobj = JObject.Parse(body);
        }

        public async void RequestAuthTokenAsync(string phoneNumber)
        {
            var response = await ServiceHelpers.GetAsync("/auth_token/{0}");
            var body = await response.Content.ReadAsStringAsync();
            JObject jobj = JObject.Parse(body);
        }


    }
}
