namespace Thoughtology.GameOfLife.Web
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using App_Start;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
