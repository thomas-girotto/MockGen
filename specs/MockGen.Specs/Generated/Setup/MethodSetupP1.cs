using MockGen.Matcher;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Setup
{
    internal abstract class MethodSetup<TParam1> : MethodSetupBase, IMethodSetup<TParam1>
    {
        protected List<TParam1> calls = new List<TParam1>();
        protected new ActionConfiguration<TParam1> currentConfiguration;

        protected void ForParameter(Arg<TParam1> paramValue)
        {
            ClearCurrentConfiguration();
            currentConfiguration = new ActionConfiguration<TParam1>(base.currentConfiguration);
            currentConfiguration.Matcher1 = ArgMatcher<TParam1>.Create(paramValue);
        }

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
                return calls.Where(param => currentConfiguration.Match(param));
            }
        }

        public void Execute(Action<TParam1> callback)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Execute));
            currentConfiguration.ExecuteAction = callback;
        }
    }
}
