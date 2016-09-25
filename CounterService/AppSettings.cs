using System;
using System.Configuration;
using CounterService.Persistence;
using CounterService.Persistence.MySql;

namespace CounterService
{
    internal static class AppSettings
    {
        private static string Get(string key, string defaultValue = null)
        {
            return Environment.GetEnvironmentVariable(key) ?? ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        public static string ConnectionString
        {
            get
            {
                var connStr = Get("ConnectionString");
                if (string.IsNullOrWhiteSpace(connStr)) throw new InvalidOperationException("'ConnectionString' is not set");
                return connStr;
            }
        }

        private static ICounter _persistence;
        public static ICounter Persistence => _persistence ?? (_persistence = new MysqlNativeCounter(ConnectionString)); // if there are multiple persistence implemetations, make them configurable here.
    }
}