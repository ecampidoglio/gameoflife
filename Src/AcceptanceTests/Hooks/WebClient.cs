namespace Thoughtology.GameOfLife.AcceptanceTests.Hooks
{
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class WebClient
    {
        private readonly HttpClient client;

        public WebClient()
        {
            client = new HttpClient { BaseAddress = WebServer.RootUri };
            AddAcceptJsonHeader();
        }

        public HttpResponseMessage Get(string uri)
        {
            return client.GetAsync(uri).Result;
        }

        public HttpResponseMessage PostAsJson(string uri, object data)
        {
            return client.PostAsJsonAsync(uri, data).Result;
        }

        private void AddAcceptJsonHeader()
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
