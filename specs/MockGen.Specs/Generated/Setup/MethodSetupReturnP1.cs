using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TParam1, TReturn> : MethodSetup<TParam1>, IMethodSetupReturn<TParam1, TReturn>
    {
        private Stack<ActionConfigurationWithReturn<TParam1, TReturn>> configuredActions = new Stack<ActionConfigurationWithReturn<TParam1, TReturn>>();
        private new ActionConfigurationWithReturn<TParam1, TReturn> currentConfiguration;

        public new IMethodSetupReturn<TParam1, TReturn> ForParameter(Arg<TParam1> paramValue)
        {
            base.ForParameter(paramValue);
            if (!IsSetupDone)
            {
                currentConfiguration = new ActionConfigurationWithReturn<TParam1, TReturn>(base.currentConfiguration);
                configuredActions.Push(currentConfiguration);
            }

            return this;
        }

        public IMethodSetup<TParam1> Returns(TReturn value)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Returns));
            currentConfiguration.ReturnAction = () => value;
            return this;
        }

        public TReturn ExecuteSetup(TParam1 param1)
        {
            calls.Add(param1);
            foreach (var setup in configuredActions)
            {
                if (setup.Match(param1))
                {
                    return setup.RunActions(param1);
                }
            }

            return default(TReturn);
        }
    }
}
