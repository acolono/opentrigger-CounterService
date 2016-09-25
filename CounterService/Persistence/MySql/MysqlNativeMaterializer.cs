using CounterService.Models;
using MySql.Data.MySqlClient;

namespace CounterService.Persistence.MySql
{
    public static class MysqlNativeMaterializer 
    {
        /// <summary>
        /// Read first tCounter
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static tCounter MaterializeCounter(this MySqlCommand cmd)
        {
            tCounter counter = null;
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    counter = new tCounter
                    {
                        guid = reader.GetGuid("guid"),
                        ts = reader.GetDateTime("ts"),
                        value = reader.GetInt64("value"),
                    };
                }
            }
            return counter;
        }
    }
}