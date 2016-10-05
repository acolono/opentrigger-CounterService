using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using dncCnt.Models;
using MySql.Data.MySqlClient;

namespace dncCnt.Persistence.MySql
{
    /// <summary>
    /// Mysql Implementation
    /// </summary>
    public class MysqlAsyncAdoCounter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public MysqlAsyncAdoCounter(string connectionString = null)
        {
            _connectionString = MySqlInit.SetupDatabase(connectionString);
        }
        private readonly string _connectionString;

        /// <summary>
        /// Get the counter value
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns>Counter</returns>
        public async Task<tCounter> Get(Guid guid)
        {
            tCounter c;
            using (var db = new MySqlConnection(_connectionString))
            {
                await db.OpenAsync();
                using (var cmd = new MySqlCommand("select * from counter where guid=@guid", db))
                {
                    cmd.Parameters.Add("@guid", DbType.Binary, guid.ToByteArray());
                    c = (await cmd.MaterializeCounterAync()).FirstOrDefault();
                }
                
            }
            return c ?? await Set(guid);
        }


        /// <summary>
        /// Increment the counter
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="by"></param>
        /// <returns>Counter</returns>
        public async Task<tCounter> Increment(Guid guid, long by = 1)
        {
            tCounter c;
            const string sql = "update counter set value=value+@by where guid=@guid;" +
                               "select * from counter where guid=@guid";

            using (var db = new MySqlConnection(_connectionString))
            {
                await db.OpenAsync();
                using (var cmd = new MySqlCommand(sql, db))
                {
                    cmd.Parameters.Add("@guid", DbType.Binary, guid.ToByteArray());
                    cmd.Parameters.Add("@by", DbType.Int64, by);
                    c = (await cmd.MaterializeCounterAync()).FirstOrDefault();
                }

            }
            return c ?? await Set(guid, by);
        }

        /// <summary>
        /// Set counter value or create a new one
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="value">value</param>
        /// <returns>64bit number</returns>
        public async Task<tCounter> Set(Guid guid, long value = 0)
        {
            using (var db = new MySqlConnection(_connectionString))
            {
                await db.OpenAsync();
                const string sql = "replace into counter (guid,value) values (@guid,@value);" +
                                   "select * from counter where guid=@guid;";

                using (var cmd = new MySqlCommand(sql, db))
                {
                    cmd.Parameters.Add("@guid", DbType.Binary, guid.ToByteArray());
                    cmd.Parameters.Add("@value", DbType.Int64, value);
                    return (await cmd.MaterializeCounterAync()).Single();
                }
            }
        }
    }
}
