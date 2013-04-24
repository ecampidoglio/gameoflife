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

        public HttpResponseMessage Get(string uri, params object[] uriValues)
        {
            uri = ExpandVariablesInUri(uri, uriValues);
            return client.GetAsync(uri).Result;
        }

        public HttpResponseMessage PutAsJson(string uri, object data, params object[] uriValues)
        {
            uri = ExpandVariablesInUri(uri, uriValues);
            return client.PutAsJsonAsync(uri, data).Result;
        }

        public HttpResponseMessage PostAsJson(string uri, object data, params object[] uriValues)
        {
            uri = ExpandVariablesInUri(uri, uriValues);
            return client.PostAsJsonAsync(uri, data).Result;
        }

        public HttpResponseMessage Delete(string uri, params object[] uriValues)
        {
            uri = ExpandVariablesInUri(uri, uriValues);
            return client.DeleteAsync(uri).Result;
        }

        private void AddAcceptJsonHeader()
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private string ExpandVariablesInUri(string uri, object[] arguments)
        {
            return string.Format(uri, arguments);
        }
    }
}
