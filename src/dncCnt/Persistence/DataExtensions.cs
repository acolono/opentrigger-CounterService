using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using dncCnt.Models;

namespace dncCnt.Persistence
{
    public static class DataExtensions 
    {

        public static async Task<T> GetFieldValueAsync<T>(this DbDataReader reader, string name)
        {
            var column = reader.GetOrdinal(name);
            return await reader.GetFieldValueAsync<T>(column);
        }
        public static T GetFieldValue<T>(this DbDataReader reader, string name)
        {
            var column = reader.GetOrdinal(name);
            return reader.GetFieldValue<T>(column);
        }

        public static async Task<List<tCounter>> MaterializeCounterAync<T>(this T cmd) where T:DbCommand
        {
            var counters = new List<tCounter>();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while(await reader.ReadAsync())
                {
                    counters.Add(new tCounter
                    {
                        guid = new Guid(await reader.GetFieldValueAsync<byte[]>("guid")),
                        value = await reader.GetFieldValueAsync<long>("value"),
                        ts = await reader.GetFieldValueAsync<DateTime>("ts"),
                    });
                }
            }
            return counters;
        }

        public static List<tCounter> MaterializeCounter<T>(this T cmd) where T : DbCommand
        {
            var counters = new List<tCounter>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    counters.Add(new tCounter
                    {
                        guid = new Guid(reader.GetFieldValue<byte[]>("guid")),
                        value = reader.GetFieldValue<long>("value"),
                        ts = reader.GetFieldValue<DateTime>("ts"),
                    });
                }
            }
            return counters;
        }

    }
}