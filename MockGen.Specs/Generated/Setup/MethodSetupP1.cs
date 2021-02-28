using MockGen.Matcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Setup
{
    /// <summary>
    /// Common implementation between MethodSetupVoidP and MethodSetupReturnP
    /// </summary>
    internal abstract class MethodSetup<TParam1> : IMethodSetup<TParam1>
    {
        protected List<TParam1> calls = new List<TParam1>();
        protected ArgMatcher<TParam1> matcher;

        public int NumberOfCalls => MatchingCalls.Count();

        public IEnumerable<TParam1> MatchingCalls => calls.Where(param => matcher.Match(param));

        public abstract void Throws<TException>() where TException : Exception, new();

        public abstract void Throws<TException>(TException exception) where TException : Exception;
    }
}
