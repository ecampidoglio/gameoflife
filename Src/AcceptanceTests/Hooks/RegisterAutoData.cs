using BoDi;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using TechTalk.SpecFlow;

namespace Cerdo.CNA.Lennart.AcceptanceTests.Hooks
{
    [Binding]
    public class RegisterAutoData
    {
        private readonly IObjectContainer _container;

        public RegisterAutoData(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void RegisterAFixtureInstance()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoRhinoMockCustomization());
            _container.RegisterInstanceAs<IFixture>(fixture);
        }
    }
}
