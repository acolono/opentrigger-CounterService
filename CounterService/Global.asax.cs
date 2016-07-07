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
        }

        /// <summary>
        /// Global Error Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, System.EventArgs e)
        {
            var exception = Server.GetLastError();
            System.Console.WriteLine(exception.ToString());
        }
    }
}
