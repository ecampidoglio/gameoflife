namespace Thoughtology.GameOfLife.Tests.Foundation
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Hosting;

    public class ApiControllerFactory
    {
        public static TController Create<TController>(Func<TController> constructor)
            where TController : ApiController
        {
            var controller = constructor();
            AssignEmptyRequest(controller);
            return controller;
        }

        private static void AssignEmptyRequest(ApiController controller)
        {
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(
                HttpPropertyKeys.HttpConfigurationKey,
                new HttpConfiguration());
        }
    }
}
