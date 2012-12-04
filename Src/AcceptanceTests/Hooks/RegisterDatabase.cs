using System.Configuration;
using BoDi;
using Cerdo.CNA.Lennart.Common.Infrastructure;
using Cerdo.CNA.Lennart.Web.App_Start;
using TechTalk.SpecFlow;

namespace Cerdo.CNA.Lennart.AcceptanceTests.Hooks
{
    [Binding]
    public class RegisterDatabase
    {
        private readonly IObjectContainer _container;

        public RegisterDatabase(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario("DependsOnDatabase")]
        public void RegisterADatabaseInstance()
        {
            var connString = GetConnectionStringFromConfig();
            _container.RegisterInstanceAs<IDatabase>(new OrmDatabase(connString));
        }

        private static string GetConnectionStringFromConfig()
        {
            return ConfigurationManager
                .ConnectionStrings[WebApiConfig.ConnectionStringName]
                .ConnectionString;
        }
    }
}
