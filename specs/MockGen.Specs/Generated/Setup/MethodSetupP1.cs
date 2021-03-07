using MockGen.Matcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Setup
{
    internal abstract class MethodSetup<TParam1> : MethodSetupBase, IMethodSetup<TParam1>
    {
        protected List<TParam1> calls = new List<TParam1>();
        protected ArgMatcher<TParam1> matcher;

        public int NumberOfCalls 
        { 
            get 
            {
                EnsureSpyingMethodsAreAllowed(nameof(NumberOfCalls));
                return MatchingCalls.Count();
            }
        } 

        public IEnumerable<TParam1> MatchingCalls
        {
            get
            {
                EnsureSpyingMethodsAreAllowed(nameof(MatchingCalls));
                return calls.Where(param => matcher.Match(param));
            }
        }

        public abstract void Execute(Action<TParam1> callback);
        public abstract void Throws<TException>() where TException : Exception, new();

        public abstract void Throws<TException>(TException exception) where TException : Exception;
    }
}
