using System;

namespace MockGen.Setup
{
    internal class ActionConfiguration
    {
        private readonly ActionConfigurationBase baseConfiguration;

        internal ActionConfiguration(ActionConfigurationBase baseConfiguration)
        {
            this.baseConfiguration = baseConfiguration;
        }

        internal Action ExecuteAction { private get; set; }

        internal void RunActions()
        {
            baseConfiguration.ThrowAction?.Invoke();
            ExecuteAction?.Invoke();
        }
    }
}
