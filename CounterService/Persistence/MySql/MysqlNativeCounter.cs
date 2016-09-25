using System;
using CounterService.Models;
using MySql.Data.MySqlClient;

namespace CounterService.Persistence.MySql
{
    /// <summary>
    /// Mysql Implementation
    /// </summary>
    public class MysqlNativeCounter : ICounter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public MysqlNativeCounter(string connectionString)
        {
            _connectionString = connectionString;
            using (var db = new MySqlConnection(_connectionString))
            {
                db.Open();
                using (var cmd = new MySqlCommand(MysqlInit.Sql, db))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private readonly string _connectionString;

        /// <summary>
        /// Get the counter value
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns>Counter</returns>
        public tCounter Get(Guid guid)
        {
            tCounter c;
            using (var db = new MySqlConnection(_connectionString))
            {
                db.Open();
                using (var cmd = new MySqlCommand("select * from counter where guid=@guid", db))
                {
                    cmd.Parameters.AddWithValue("@guid", guid);
                    c = cmd.MaterializeCounter();
                }
                
            }
            return c ?? Set(guid);
        }


        /// <summary>
        /// Increment the counter
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="by"></param>
        /// <returns>Counter</returns>
        public tCounter Increment(Guid guid, long by = 1)
        {
            tCounter c;
            const string sql = "update counter set value=value+@by where guid=@guid;" +
                               "select * from counter where guid=@guid";

            using (var db = new MySqlConnection(_connectionString))
            {
                db.Open();
                using (var cmd = new MySqlCommand(sql, db))
                {
                    cmd.Parameters.AddWithValue("@guid", guid);
                    cmd.Parameters.AddWithValue("@by", by);
                    c = cmd.MaterializeCounter();
                }

            }
            return c ?? Set(guid, by);
        }

        /// <summary>
        /// Set counter value or create a new one
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="value">value</param>
        /// <returns>64bit number</returns>
        public tCounter Set(Guid guid, long value = 0)
        {
            using (var db = new MySqlConnection(_connectionString))
            {
                db.Open();
                const string sql = "replace into counter (guid,value) values (@guid,@value);" +
                                   "select * from counter where guid=@guid;";

                using (var cmd = new MySqlCommand(sql, db))
                {
                    cmd.Parameters.AddWithValue("@guid", guid);
                    cmd.Parameters.AddWithValue("@value", value);
                    return cmd.MaterializeCounter();
                }
            }
        }
    }
}
