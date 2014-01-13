using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeClient.Helpers
{
    public static class ServiceHelpers
    {
        private static string GETUrl = "http://lyra-app.cloudapp.net/pins/{0}";
        private static string POSTUrl = "http://lyra-app.cloudapp.net/pins";

        public static async Task<HttpResponseMessage> DoPostRequest(string content)
        {
            var httpClient = new HttpClient(new HttpClientHandler());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, POSTUrl);
            request.Content = new StringContent(content);
            HttpResponseMessage response = await httpClient.SendAsync(request);

            return response;
        }

        public static async Task<HttpResponseMessage> DoGetRequest(string phoneNum)
        {
            var httpClient = new HttpClient(new HttpClientHandler());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(GETUrl, phoneNum));
            HttpResponseMessage response = await httpClient.SendAsync(request);

            return response;
        }
    }
}
