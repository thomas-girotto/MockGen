using System;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TReturn> : MethodSetup, IMethodSetupReturn<TReturn>, IReturnContinuation
    {
        private new ActionConfigurationWithReturn<TReturn> currentConfiguration;

        internal MethodSetupReturn()
        {
            currentConfiguration = new ActionConfigurationWithReturn<TReturn>(base.currentConfiguration);
        }

        public IReturnContinuation Returns(TReturn value)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Returns));
            currentConfiguration.ReturnAction = () => value;
            return this;
        }

        public IReturnContinuation Returns(Func<TReturn> returnFunc)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Returns));
            currentConfiguration.ReturnAction = returnFunc;
            return this;
        }

        public void AndExecute(Action callback)
        {
            base.Execute(callback);
        }

        public TReturn ExecuteSetup()
        {
            numberOfCalls++;
            return currentConfiguration.RunActions();
        }
    }
}
