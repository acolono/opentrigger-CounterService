using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CounterService.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace CounterService.Persistence
{
    /// <summary>
    /// Mysql Implementation
    /// </summary>
    public class MysqlCounter : ICounter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public MysqlCounter(string connectionString)
        {
            _connectionString = connectionString;
            using (var db = new MySqlConnection(_connectionString))
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
        private readonly string _connectionString;

        /// <summary>
        /// Get the counter value
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns>Counter</returns>
        public tCounter Get(Guid guid)
        {
            List<tCounter> c;
            using (var db = new MySqlConnection(_connectionString))
            {
                db.Open();
                c = db.Query<tCounter>("select * from counter where guid=@guid", new { guid }).ToList();
            }
            return !c.Any() ? Set(guid) : c.First();
        }

        /// <summary>
        /// Increment the counter
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="by"></param>
        /// <returns>Counter</returns>
        public tCounter Increment(Guid guid, long by = 1)
        {
            List<tCounter> c;
            const string sql = "update counter set value=value+@by where guid=@guid;" +
                               "select * from counter where guid=@guid";

            using (var db = new MySqlConnection(_connectionString))
            {
                db.Open();
                c = db.Query<tCounter>(sql, new { guid, by }).ToList();
            }
            return !c.Any() ? Set(guid, @by) : c.Single();
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

                return db.Query<tCounter>(sql, new { guid, value }).Single();
            }
        }
    }
}
