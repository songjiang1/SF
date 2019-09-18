using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  sys.Throttle.API
{
    public enum ThrottlePolicyType : int
    {
        IpThrottling = 1,
        ClientThrottling,
        EndpointThrottling
    }
}
