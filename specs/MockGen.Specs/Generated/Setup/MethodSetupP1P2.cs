using MockGen.Matcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Setup
{
    internal abstract class MethodSetup<TParam1, TParam2> : MethodSetupBase, IMethodSetup<TParam1, TParam2>
    {
        protected List<(TParam1 param1, TParam2 param2)> calls = new List<(TParam1 param1, TParam2 param2)>();
        protected new ActionConfiguration<TParam1, TParam2> currentConfiguration;

        protected void ForParameter(Arg<TParam1> param1, Arg<TParam2> param2)
        {
            ClearCurrentConfiguration();
            currentConfiguration = new ActionConfiguration<TParam1, TParam2>(base.currentConfiguration);
            currentConfiguration.Matcher1 = ArgMatcher<TParam1>.Create(param1);
            currentConfiguration.Matcher2 = ArgMatcher<TParam2>.Create(param2);
        }

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
                return calls.Where(arg => currentConfiguration.Match(arg.param1, arg.param2));
            }
        }

        public void Execute(Action<TParam1, TParam2> callback)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Execute));
            currentConfiguration.ExecuteAction = callback;
        }
    }
}
