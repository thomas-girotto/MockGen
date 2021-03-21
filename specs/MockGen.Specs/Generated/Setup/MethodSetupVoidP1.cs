using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupVoid<TParam> : MethodSetup<TParam>, IPropertySetSetup<TParam>
    {
        private Stack<ActionConfiguration<TParam>> configuredActions = new Stack<ActionConfiguration<TParam>>();

        public new IMethodSetup<TParam> ForParameter(Arg<TParam> paramValue)
        {
            base.ForParameter(paramValue);
            if (!IsSetupDone)
            {
                configuredActions.Push(currentConfiguration);
            }
            
            return this;
        }

        public IMethodSetup<TParam> ForValue(Arg<TParam> param) => ForParameter(param);

        public void ExecuteSetup(TParam param)
        {
            // Register call with given parameter for future assertions on calls
            calls.Add(param);
            // Execute the configured action according to given parameters
            foreach (var setup in configuredActions)
            {
                if (setup.Match(param))
                {
                    setup.RunActions(param);
                    return;
                }
            }
        }
    }
}
