using System.Net.Http;
using System.Net.Http.Headers;
using Cerdo.CNA.Lennart.AcceptanceTests.Hooks;

namespace Cerdo.CNA.Lennart.AcceptanceTests.Steps
{
    public class WebClient
    {
        private static HttpClient client;

        static WebClient()
        {
            InitializeHttpClient();
        }

        public static HttpResponseMessage PostAsJson(string uri, object data)
        {
            return client.PostAsJsonAsync(uri, data).Result;
        }

        private static void InitializeHttpClient()
        {
            client = new HttpClient { BaseAddress = WebServer.RootUri };
            AddAcceptJsonHeader();
        }

        private static void AddAcceptJsonHeader()
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
