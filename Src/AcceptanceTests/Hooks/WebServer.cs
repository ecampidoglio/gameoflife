namespace Thoughtology.GameOfLife.AcceptanceTests.Hooks
{
    using System;
    using CassiniDev;
    using TechTalk.SpecFlow;

    [Binding]
    public static class WebServer
    {
        private const string HostName = "localhost";
        private const int Port = 8090;
        private const string ApplicationPath = @"..\..\..\Web";
        private static CassiniDevServer server;

        public static Uri RootUri
        {
            get { return new Uri(string.Format("http://{0}:{1}", HostName, Port)); }
        }

        [BeforeTestRun]
        private static void Start()
        {
            server = new CassiniDevServer();
            server.StartServer(ApplicationPath, Port, "/", HostName);
        }

        [AfterTestRun]
        private static void Stop()
        {
            server.StopServer();
            server.Dispose();
        }
    }
}
