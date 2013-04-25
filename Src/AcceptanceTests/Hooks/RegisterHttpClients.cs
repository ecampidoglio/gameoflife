namespace Thoughtology.GameOfLife.AcceptanceTests.Hooks
{
    using BoDi;
    using Infrastructure;
    using TechTalk.SpecFlow;

    [Binding]
    public class RegisterHttpClients
    {
        private readonly IObjectContainer container;

        public RegisterHttpClients(IObjectContainer container)
        {
            this.container = container;
        }

        [BeforeScenario("RequireWebClient")]
        public void RegisterWebClientInstance()
        {
            container.RegisterInstanceAs(new WebClient());
        }

        [BeforeScenario("RequireWebBrowser")]
        public void RegisterWebBrowserInstance()
        {
            container.RegisterInstanceAs(new WebBrowser());
        }

        [AfterScenario("RequireWebBrowser")]
        public void ReleaseWebBrowserInstance()
        {
            container.Resolve<WebBrowser>().Close();
        }
    }
}
