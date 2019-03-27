using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VlocityAPIClientNewtonsoft
{
    public class MoveOrderMessage
    {
        public string RIOCode { get; set; }
        public string NewISP { get; set; }
        public string Offer { get; set; }
    }

    class Program
    {
        private static readonly string securityToken = ConfigurationManager.AppSettings["SecurityToken"];
        private static readonly string consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        private static readonly string consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        private static readonly string username = ConfigurationManager.AppSettings["Username"];
        private static readonly string password = ConfigurationManager.AppSettings["Password"] + securityToken;
        private static readonly string isSandboxUser = ConfigurationManager.AppSettings["IsSandboxUser"];

        private static string oauthToken;
        private static string serviceUrl;

        static void Main(string[] args)
        {
            GetOAuthToken().Wait();
            CreateMoveOrder("02P452941840", "90", "SDSL").Wait();
        }

        static async Task GetOAuthToken()
        {
            HttpClient authClient = new HttpClient();
            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"client_id", consumerKey},
                    {"client_secret", consumerSecret},
                    {"username", username},
                    {"password", password}
                }
            );

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            HttpResponseMessage message = await authClient.PostAsync("https://login.salesforce.com/services/oauth2/token", content);

            string responseString = await message.Content.ReadAsStringAsync();

            JObject obj = JObject.Parse(responseString);

            oauthToken = (string)obj["access_token"];
            serviceUrl = (string)obj["instance_url"];
        }

        static async Task CreateMoveOrder(string rioCode, string newISP, string offer)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            MoveOrderMessage moveOrderMessage = new MoveOrderMessage
            {
                RIOCode = rioCode,
                NewISP = newISP,
                Offer = offer
            };

            string requestMessage = JsonConvert.SerializeObject(moveOrderMessage);

            HttpClient moveClient = new HttpClient();
            HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

            string uri = serviceUrl + "/services/apexrest/vlocity_cmt/v1/integrationprocedure/TDC_CreateMoveOrder/";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);

            request.Headers.Add("Authorization", "Bearer " + oauthToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = content;

            HttpResponseMessage response = await moveClient.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);
        }
    }
}
