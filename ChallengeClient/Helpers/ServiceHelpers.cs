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
        public const string BASE_URL = "http://lyra-app.cloudapp.net";

        public const string GETUrl = "/pins/{0}";
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
            return await GetAsync(string.Format(GETUrl, phoneNum));
        }

        public static async Task<HttpResponseMessage> GetAsync(string url)
        {
            var httpClient = new HttpClient(new HttpClientHandler());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + url);
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
