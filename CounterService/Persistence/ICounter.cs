using System;
using CounterService.Models;

namespace CounterService.Persistence
{
    public interface ICounter
    {
        tCounter Get(Guid guid);
        tCounter Increment(Guid guid, long by = 1);
        tCounter Set(Guid guid, long value = 0);
    }
}