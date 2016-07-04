using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Dapper;
using MySql.Data.MySqlClient;

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

            using (var db = new MySqlConnection(AppSettings.ConnectionString))
            {
                db.Open();
                db.Execute(@"
                    CREATE TABLE IF NOT EXISTS `counter` (
                    `guid` CHAR(36) NOT NULL,
                    `value` BIGINT(20) NOT NULL,
                    `ts` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                    UNIQUE INDEX `idx` (`guid`)
                    )
                    ENGINE=InnoDB
                ");
            }
        }
    }
}
