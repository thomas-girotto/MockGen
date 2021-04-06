using System;
using System.Threading.Tasks;

namespace MockGen.Setup
{
    internal class MethodSetupReturnValueTask<TReturn> : MethodSetup, IMethodSetupReturn<TReturn>, IReturnContinuation
    {
        private new ActionConfigurationWithReturn<ValueTask<TReturn>> currentConfiguration;

        internal MethodSetupReturnValueTask()
        {
            currentConfiguration = new ActionConfigurationWithReturn<ValueTask<TReturn>>(base.currentConfiguration);
        }

        public IReturnContinuation Returns(TReturn value)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Returns));
            currentConfiguration.ReturnAction = () => ValueTask.FromResult(value);
            return this;
        }

        public void AndExecute(Action callback)
        {
            base.Execute(callback);
        }

        public ValueTask<TReturn> ExecuteSetup()
        {
            numberOfCalls++;
            return currentConfiguration.RunActions();
        }
    }
}
