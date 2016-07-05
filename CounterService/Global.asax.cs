using System.Web.Http;

namespace CounterService
{
    /// <summary>
    /// Main HttpApplication
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Global Startup
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AppSettings.SetupPersitence();
        }
    }
}
