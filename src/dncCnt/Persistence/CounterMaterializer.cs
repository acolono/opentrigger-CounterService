using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using dncCnt.Models;

namespace dncCnt.Persistence
{
    public static class CounterMaterializer 
    {
        /// <summary>
        /// Read tCounter
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static async Task<List<tCounter>> MaterializeCounterAync<T>(this T cmd) where T:DbCommand
        {
            var counters = new List<tCounter>();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while(await reader.ReadAsync())
                {
                    counters.Add(new tCounter
                    {
                        guid = reader.GetGuid(0),
                        value = reader.GetInt64(1),
                        ts = reader.GetDateTime(2),
                    });
                }
            }
            return counters;
        }
    }
}