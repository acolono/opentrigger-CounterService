using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using CounterService.Models;
using CounterService.Persistence;

namespace CounterService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICounterService" in both code and config file together.
    [ServiceContract]
    public interface ICounterService
    {
        [OperationContract] Guid NewGuid();
        [OperationContract] tCounter Get(Guid guid);
        [OperationContract] tCounter Increment(Guid guid, long by = 1);
        [OperationContract] tCounter Set(Guid guid, long value = 0);
    }
}
