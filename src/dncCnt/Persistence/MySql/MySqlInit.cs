using System;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace dncCnt.Persistence.MySql
{
    public static class MySqlInit
    {
        /// <summary>
        /// mysql init script
        /// </summary>
        public static string SqlTableDefinition = @"
            CREATE TABLE IF NOT EXISTS `counter` (
                `guid` VARBINARY(16) NOT NULL,
                `value` BIGINT(20) NOT NULL,
                `ts` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                UNIQUE INDEX `idx` (`guid`)
            )
            ENGINE=InnoDB
        ";

        private static readonly object Sync = new object();
        private static string _finalConnectionString = null;

        public static string SetupDatabase(string connectionString)
        {
            if (_finalConnectionString != null) return _finalConnectionString;
            lock (Sync)
            {
                if (_finalConnectionString != null) return _finalConnectionString;
                if (string.IsNullOrWhiteSpace(connectionString)) connectionString = Environment.GetEnvironmentVariable("connectionString");
                var cb = new MySqlConnectionStringBuilder(connectionString);
                var database = cb.Database;
                cb.Database = null;
                var noDbConnectionString = cb.GetConnectionString(true);

                if (!Regex.IsMatch(database, "[0-9A-Z]+", RegexOptions.IgnoreCase))
                {
                    throw new ArgumentOutOfRangeException($"invalid database name: {database ?? "<null>"}");
                }

                using (var db = new MySqlConnection(noDbConnectionString))
                {
                    db.Open();
                    var sql = $"CREATE DATABASE IF NOT EXISTS {database}";
                    using (var cmd = new MySqlCommand(sql, db))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                using (var db = new MySqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = new MySqlCommand(SqlTableDefinition, db))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return _finalConnectionString = connectionString; 
            }
        }
    }
}