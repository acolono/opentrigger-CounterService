using System.Data;
using MySql.Data.MySqlClient;

namespace dncCnt.Persistence.MySql
{
    public static class MySqlCommandExtensions
    {
        public static void Add(this MySqlParameterCollection p, string name, DbType type, object value)
        {
            p.Add(name, type);
            p[name].Value = value;
        }
    }
}