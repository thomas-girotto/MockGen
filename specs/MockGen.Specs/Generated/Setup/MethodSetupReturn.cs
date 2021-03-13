using System;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TReturn> : MethodSetup, IMethodSetupReturn<TReturn>
    {
        private new ActionConfigurationWithReturn<TReturn> currentConfiguration;

        internal MethodSetupReturn()
        {
            currentConfiguration = new ActionConfigurationWithReturn<TReturn>(base.currentConfiguration);
        }

        public void Returns(TReturn value)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Returns));
            currentConfiguration.ReturnAction = () => value;
        }

        public TReturn ExecuteSetup()
        {
            numberOfCalls++;
            return currentConfiguration.RunActions();
        }
    }
}
