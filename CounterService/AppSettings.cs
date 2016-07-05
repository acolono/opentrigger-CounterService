using System;
using System.Configuration;
using CounterService.Persistence;

namespace CounterService
{
    internal static class AppSettings
    {
        static string Get(string key, string defaultValue = null)
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

        public static ICounter Persistence { get; private set; }

        public static void SetupPersitence()
        {
            // if there are multiple persistence implemetations, make them configurable here.
            Persistence = new MysqlCounter(ConnectionString);
        }
    }
}