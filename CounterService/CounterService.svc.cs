using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CounterService.Models;
using CounterService.Persistence;

namespace CounterService
{
    public class CounterService : ICounterService, ICounter
    {
        public tCounter Get(Guid guid) => AppSettings.Persistence.Get(guid);

        public tCounter Increment(Guid guid, long @by = 1) => AppSettings.Persistence.Increment(guid, @by);

        public tCounter Set(Guid guid, long value = 0) => AppSettings.Persistence.Set(guid, value);

        public Guid NewGuid() => Guid.NewGuid();
    }
}
