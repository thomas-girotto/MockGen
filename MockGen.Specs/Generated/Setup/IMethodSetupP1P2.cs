using System.Collections.Generic;

namespace MockGen.Setup
{
    internal interface IMethodSetup<TParam1, TParam2> : IMethodSetup
    {
        IEnumerable<(TParam1 param1, TParam2 param2)> MatchingCalls { get; }
    }
}
