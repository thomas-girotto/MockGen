using MockGen.Matcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Setup
{
    internal abstract class MethodSetup<TParam1, TParam2> : MethodSetupBase, IMethodSetup<TParam1, TParam2>
    {
        protected List<(TParam1 param1, TParam2 param2)> calls = new List<(TParam1 param1, TParam2 param2)>();
        protected ArgMatcher<TParam1> matcher1;
        protected ArgMatcher<TParam2> matcher2;

        public int NumberOfCalls
        {
            get
            {
                EnsureSpyingMethodsAreAllowed(nameof(NumberOfCalls));
                return MatchingCalls.Count();
            }
        }

        public IEnumerable<(TParam1 param1, TParam2 param2)> MatchingCalls
        {
            get
            {
                EnsureSpyingMethodsAreAllowed(nameof(MatchingCalls));
                return calls.Where(arg => 
                    matcher1.Match(arg.param1) && 
                    matcher2.Match(arg.param2));
            }
        }

        public abstract void Throws<TException>() where TException : Exception, new();

        public abstract void Throws<TException>(TException exception) where TException : Exception;

        public abstract void Execute(Action<TParam1, TParam2> callback);
    }
}
