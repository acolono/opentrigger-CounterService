using System;
using System.Configuration;

namespace CounterService
{
    internal static class AppSettings
    {
        static string Get(string key, string defaultValue = null)
        {
            return Environment.GetEnvironmentVariable(key) ?? ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                var connStr = Get("ConnectionString");
                if (string.IsNullOrWhiteSpace(connStr)) throw new InvalidOperationException("'ConnectionString' is not set");
                return connStr;
            }
        }
    }
}