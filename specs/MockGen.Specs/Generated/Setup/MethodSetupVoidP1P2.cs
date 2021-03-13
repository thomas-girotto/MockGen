using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupVoid<TParam1, TParam2> : MethodSetup<TParam1, TParam2>
    {
        private Stack<ActionConfiguration<TParam1, TParam2>> configuredActions = new Stack<ActionConfiguration<TParam1, TParam2>>();

        public new IMethodSetup<TParam1, TParam2> ForParameter(Arg<TParam1> param1, Arg<TParam2> param2)
        {
            base.ForParameter(param1, param2);
            if (!IsSetupDone)
            {
                configuredActions.Push(currentConfiguration);
            }

            return this;
        }

        public void ExecuteSetup(TParam1 param1, TParam2 param2)
        {
            // Register call with given parameter for future assertions on calls
            calls.Add((param1, param2));
            // Execute the configured action according to given parameters
            foreach (var setup in configuredActions)
            {
                if (setup.Match(param1, param2))
                {
                    setup.RunActions(param1, param2);
                    return;
                }
            }
        }

    }
}
