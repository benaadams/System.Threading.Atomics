using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Threading.Atomic
{
    public enum MemoryOrder
    {
        Relaxed,
        Consume,
        Acquire,
        Release,
        AcquireRelease,
        SequentiallyConsistent
    }
}
