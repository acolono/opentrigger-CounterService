using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using CounterService.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace CounterService.Controllers
{

    public class CounterController : ApiController
    {
        /// <summary>
        /// Get the counter value
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns>Counter</returns>
        [HttpGet]
        public tCounter Get(Guid guid)
        {
            return AppSettings.Persistence.Get(guid);
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
            return AppSettings.Persistence.Increment(guid, by);
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
            return AppSettings.Persistence.Set(guid.Value, value);
        }
    }
}
