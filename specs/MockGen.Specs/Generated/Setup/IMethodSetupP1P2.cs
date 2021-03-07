using System;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal interface IMethodSetup<TParam1, TParam2> : IMethodSetupBase
    {
        IEnumerable<(TParam1 param1, TParam2 param2)> MatchingCalls { get; }
        void Execute(Action<TParam1, TParam2> callback);
    }
}
