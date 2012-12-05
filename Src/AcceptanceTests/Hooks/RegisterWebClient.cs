namespace Thoughtology.GameOfLife.AcceptanceTests.Hooks
{
    using BoDi;
    using TechTalk.SpecFlow;

    [Binding]
    public class RegisterWebClient
    {
        private readonly IObjectContainer container;

        public RegisterWebClient(IObjectContainer container)
        {
            this.container = container;
        }

        [BeforeScenario]
        public void RegisterWebClientInstance()
        {
            container.RegisterInstanceAs(new WebClient());
        }
    }
}
