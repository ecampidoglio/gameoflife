namespace Thoughtology.GameOfLife.Tests.Foundation
{
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Hosting;
    using System.Web.Mvc;
    using Ploeh.AutoFixture;

    public class ControllerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(EmptyHttpRequest);
            fixture.Customize<ControllerContext>(c => c.OmitAutoProperties());
        }

        private static HttpRequestMessage EmptyHttpRequest()
        {
            var request = new HttpRequestMessage();
            request.Properties.Add(
                HttpPropertyKeys.HttpConfigurationKey,
                new HttpConfiguration());
            return request;
        }
    }
}
