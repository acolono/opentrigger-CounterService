using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Dapper;
using MySql.Data.MySqlClient;

namespace CounterService.Controllers
{

    public class CounterController : ApiController
    {
        private static readonly string ConnectionString = AppSettings.ConnectionString;
        public class tCounter { public Guid guid; public long value; public DateTimeOffset ts; }

        /// <summary>
        /// Get the counter value
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns>Counter</returns>
        [HttpGet]
        public tCounter Get(Guid guid)
        {
            using (var db = new MySqlConnection(ConnectionString))
            {
                db.Open();
                var c = db.Query<tCounter>("select * from counter where guid=@guid", new { guid }).ToList();
                if (!c.Any()) throw new Exception("Counter does not exist, use Set to initialize");
                return c.First();
            }
        }

        /// <summary>
        /// Increment the counter
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="by"></param>
        /// <returns>Counter</returns>
        [HttpPost]
        public tCounter Increment(Guid guid, long by = 1)
        {
            using (var db = new MySqlConnection(ConnectionString))
            {
                db.Open();
                const string sql = "update counter set value=value+@by where guid=@guid;" +
                                   "select * from counter where guid=@guid";
                var c = db.Query<tCounter>(sql, new { guid, by }).ToList();
                if (!c.Any()) throw new Exception("Counter does not exist, use Set to initialize");
                return c.Single();
            }
        }

        /// <summary>
        /// Set counter value or create a new one
        /// </summary>
        /// <param name="guid">guid or empty (00000000-0000-0000-0000-000000000000)</param>
        /// <param name="value">value</param>
        /// <returns>64bit number</returns>
        [HttpPost]
        public tCounter Set(Guid? guid = null, long value = 0)
        {
            if (!guid.HasValue || guid.Value == Guid.Empty) guid = Guid.NewGuid();
            using (var db = new MySqlConnection(ConnectionString))
            {
                db.Open();
                const string sql = "replace into counter (guid,value) values (@guid,@value);" +
                                   "select * from counter where guid=@guid;";

                return db.Query<tCounter>(sql, new { guid, value }).Single();
            }
        }
    }
}
