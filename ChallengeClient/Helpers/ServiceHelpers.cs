using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeClient.Helpers
{
    public static class ServiceHelpers
    {
        public const string BASE_URL = "http://lyra-app.cloudapp.net";
        // public const string BASE_URL = "http://192.168.1.27:5000";

        public const string GETUrl = "/pins";
        public const string POSTUrl = "/pins";

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
            return await GetAsync(GETUrl);
        }

        public static async Task<HttpResponseMessage> GetAsync(string url)
        {
            var handler = new HttpClientHandler();

            var localSettings = IsolatedStorageSettings.ApplicationSettings;
            if (localSettings.Contains("authToken"))
            {
                // Using auth token as the username
                var authToken = localSettings["authToken"] as string;
                handler.Credentials = new NetworkCredential(authToken, "none");
            }

            var httpClient = new HttpClient(handler);
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + url);
            Debug.WriteLine(request.ToString());
            HttpResponseMessage response = await httpClient.SendAsync(request);

            return response;
        }
        public static async Task<HttpResponseMessage> PostAsync(string url, string content)
        {
            var httpClient = new HttpClient(new HttpClientHandler());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BASE_URL + url);
            request.Content = new StringContent(content);
            HttpResponseMessage response = await httpClient.SendAsync(request);

            return response;
        }
    }
}
