using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterService.Models
{
    public class tCounter
    {
        public Guid guid;
        public long value;
        public DateTimeOffset ts;
    }
}