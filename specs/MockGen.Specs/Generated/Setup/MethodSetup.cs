using System;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal abstract class MethodSetup : MethodSetupBase, IMethodSetup
    {
        protected int numberOfCalls;
        protected new ActionConfiguration currentConfiguration;

        internal MethodSetup()
        {
            currentConfiguration = new ActionConfiguration(base.currentConfiguration);
        }

        public int NumberOfCalls
        {
            get
            {
                EnsureSpyingMethodsAreAllowed(nameof(NumberOfCalls));
                return numberOfCalls;
            }
        }

        public void Execute(Action callback)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Execute));
            currentConfiguration.ExecuteAction = callback;
        }
    }
}
