using System;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal interface IMethodSetup<TParam> : IMethodSetupBase
    {
        IEnumerable<TParam> MatchingCalls { get; }
        void Execute(Action<TParam> callback);
    }
}
