using System;

namespace MockGen.Setup
{
    internal class ActionConfigurationWithReturn<TParam1, TReturn>
    {
        private ActionConfiguration<TParam1> baseConfiguration;

        internal ActionConfigurationWithReturn(ActionConfiguration<TParam1> baseConfiguration)
        {
            this.baseConfiguration = baseConfiguration;
        }

        internal Func<TReturn> ReturnAction { private get; set; } = () => default(TReturn);

        internal bool Match(TParam1 param1) => baseConfiguration.Match(param1);

        internal TReturn RunActions(TParam1 param1)
        {
            baseConfiguration.RunActions(param1);
            return ReturnAction();
        }
    }
}
