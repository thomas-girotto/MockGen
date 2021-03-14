using System;

namespace MockGen.Setup
{
    internal class ActionConfigurationWithReturn<TReturn>
    {
        private readonly ActionConfiguration baseConfiguration;

        internal ActionConfigurationWithReturn(ActionConfiguration baseConfiguration)
        {
            this.baseConfiguration = baseConfiguration;
        }

        internal Func<TReturn> ReturnAction { private get; set; } = () => default(TReturn);

        internal TReturn RunActions()
        {
            baseConfiguration.RunActions();
            return ReturnAction();
        }
    }
}
