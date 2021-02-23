using System.Collections.Generic;
using System.Linq;

namespace MockGen.Specs.Generated.Helpers
{
    internal class MethodSpy
    {
        private int totalCalls;

        internal void WasCalled()
        {
            totalCalls++;
        }

        internal int TotalCalls => totalCalls;
    }

    internal class MethodSpy<TParam>
    {
        private List<TParam> calls = new List<TParam>();

        internal void RegisterCallParameters(TParam paramValue)
        {
            calls.Add(paramValue);
        }

        internal IEnumerable<TParam> GetMatchingCalls(ArgMatcher<TParam> matcher) 
        {
            return calls.Where(arg => matcher.Match(arg));
        }
    }

    internal class MethodSpy<TParam1, TParam2>
    {
        private List<(TParam1 param1, TParam2 param2)> calls = new List<(TParam1, TParam2)>();

        internal void RegisterCallParameters(TParam1 param1, TParam2 param2)
        {
            calls.Add((param1, param2));
        }

        internal IEnumerable<(TParam1, TParam2)> GetMatchingCalls(ArgMatcher<TParam1> matcher1, ArgMatcher<TParam2> matcher2)
        {
            return calls.Where(arg => matcher1.Match(arg.param1) && matcher2.Match(arg.param2));
        }
    }
}
