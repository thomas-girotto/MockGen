using MockGen.Matcher;
using System;

namespace MockGen.Setup
{
    internal class ActionConfiguration<TParam1>
    {
        private readonly ActionConfigurationBase baseConfiguration;

        public ActionConfiguration(ActionConfigurationBase baseConfiguration)
        {
            this.baseConfiguration = baseConfiguration;
        }

        internal ArgMatcher<TParam1> Matcher1 { private get; set; } = new AnyArgMatcher<TParam1>();
        
        internal Action<TParam1> ExecuteAction { private get; set; }

        internal bool Match(TParam1 param1) => Matcher1.Match(param1);

        internal void RunActions(TParam1 param1)
        {
            baseConfiguration.ThrowAction?.Invoke();
            ExecuteAction?.Invoke(param1);
        }
    }
}
