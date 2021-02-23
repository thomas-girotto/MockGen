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
}
