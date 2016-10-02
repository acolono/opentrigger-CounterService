using System;
using System.Threading.Tasks;
using dncCnt.Models;
using dncCnt.Persistence.MySql;
using Microsoft.AspNetCore.Mvc;

namespace dncCnt.Controllers
{
    public class CounterController : Controller
    {
        private readonly MysqlAdoCounter _db;

        public CounterController()
        {
            _db = new MysqlAdoCounter();
        }

        /// <summary>
        /// Get the counter value
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns>Counter</returns>
        [Route("Counter/Get/{guid}")]
        [HttpGet]
        public async Task<tCounter> Get([FromRoute]Guid guid)
        {
            return await _db.Get(guid.NewIfEmpty());
        }

        /// <summary>
        /// Increment the counter
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="by"></param>
        /// <returns>Counter</returns>
        [HttpGet, HttpPatch]
        [Route("Counter/Increment/{guid}")]
        public async Task<tCounter> Increment([FromRoute]Guid guid, [FromQuery]long by = 1)
        {
            return await _db.Increment(guid.NewIfEmpty(), by);
        }

        /// <summary>
        /// Set counter value or create a new one
        /// </summary>
        /// <param name="guid">guid or empty (00000000-0000-0000-0000-000000000000)</param>
        /// <param name="value">value</param>
        /// <returns>64bit number</returns>
        [HttpGet, HttpPost]
        [Route("Counter/Set/{guid}")]
        public async Task<tCounter> Set([FromRoute]Guid guid, [FromQuery]long value = 0)
        {
            return await _db.Set(guid.NewIfEmpty(), value);
        }
    }
}