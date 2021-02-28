using System.Collections.Generic;

namespace MockGen.Setup
{
    internal interface IMethodSetup<TParam> : IMethodSetup
    {
        IEnumerable<TParam> MatchingCalls { get; }
    }
}
