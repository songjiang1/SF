using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  sys.Throttle.API
{
    public enum RateLimitPeriod
    {
        Second = 1,
        Minute,
        Hour,
        Day,
        Week
    }
}
