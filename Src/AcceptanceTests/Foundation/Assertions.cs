namespace Thoughtology.GameOfLife.AcceptanceTests.Foundation
{
    using System.Net;
    using System.Net.Http;
    using NUnit.Framework;

    public static class Assertions
    {
        public static void ShouldBeSuccessful(this HttpResponseMessage response)
        {
            response.StatusCode.Should(Is.InRange(HttpStatusCode.OK, HttpStatusCode.MultipleChoices));
        }
    }
}
