using MockGen.Matcher;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TParam1, TParam2, TReturn> : MethodSetup<TParam1, TParam2>, IMethodSetupReturn<TParam1, TParam2, TReturn>
    {
        private Stack<ActionConfigurationWithReturn<TParam1, TParam2, TReturn>> configuredActions = new Stack<ActionConfigurationWithReturn<TParam1, TParam2, TReturn>>();
        private new ActionConfigurationWithReturn<TParam1, TParam2, TReturn> currentConfiguration;

        public new IMethodSetupReturn<TParam1, TParam2, TReturn> ForParameter(Arg<TParam1> param1, Arg<TParam2> param2)
        {
            base.ForParameter(param1, param2);
            if (!IsSetupDone)
            {
                currentConfiguration = new ActionConfigurationWithReturn<TParam1, TParam2, TReturn>(base.currentConfiguration);
                configuredActions.Push(currentConfiguration);
            }
            
            return this;
        }

        public IMethodSetup<TParam1, TParam2> Returns(TReturn value)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Returns));
            currentConfiguration.ReturnAction = () => value;
            return this;
        }

        public TReturn ExecuteSetup(TParam1 param1, TParam2 param2)
        {
            // Register call with given parameter for future assertions on calls
            calls.Add((param1, param2));
            // Execute the configured action according to given parameters
            foreach (var setup in configuredActions)
            {
                if (setup.Match(param1, param2))
                {
                    return setup.RunActions(param1, param2);
                }
            }
            // If we didn't found any setup action to do, execute the default one.
            return default(TReturn);
        }
    }
}
