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

        public async Task<string> RequestAuthTokenAsync(string validationCode)
        {
            var response = await ServiceHelpers.GetAsync(string.Format("/auth_token/{0}", validationCode));
            var body = await response.Content.ReadAsStringAsync();

            try
            {
                JObject jobj = JObject.Parse(body);
                return jobj["user"]["auth_token"].Value<string>();
            }
            catch (Exception)
            {
                return null;
            }
            
        }


    }
}
