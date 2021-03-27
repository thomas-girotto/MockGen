using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockGen.Setup
{
    internal class MethodSetupReturnTask<TReturn> : MethodSetup, IMethodSetupReturn<TReturn>, IReturnContinuation
    {
        private new ActionConfigurationWithReturn<Task<TReturn>> currentConfiguration;

        internal MethodSetupReturnTask()
        {
            currentConfiguration = new ActionConfigurationWithReturn<Task<TReturn>>(base.currentConfiguration);
        }

        public IReturnContinuation Returns(TReturn value)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Returns));
            currentConfiguration.ReturnAction = () => Task.FromResult(value);
            return this;
        }

        public void AndExecute(Action callback)
        {
            base.Execute(callback);
        }

        public Task<TReturn> ExecuteSetup()
        {
            numberOfCalls++;
            return currentConfiguration.RunActions();
        }
    }
}
