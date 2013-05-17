namespace Thoughtology.GameOfLife.AcceptanceTests.Foundation
{
    using System.Net.Http;

    public static class HttpExtensions
    {
        public static T ReadContentAs<T>(this HttpResponseMessage response)
        {
            return response.Content.ReadAsAsync<T>().Result;
        }
    }
}
