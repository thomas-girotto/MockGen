using MockGen.Matcher;
using System;

namespace MockGen.Setup
{
    internal class ActionConfiguration<TParam1, TParam2>
    {
        private readonly ActionConfigurationBase baseConfiguration;

        public ActionConfiguration(ActionConfigurationBase baseConfiguration)
        {
            this.baseConfiguration = baseConfiguration;
        }

        internal ArgMatcher<TParam1> Matcher1 { private get; set; } = new AnyArgMatcher<TParam1>();
        internal ArgMatcher<TParam2> Matcher2 { private get; set; } = new AnyArgMatcher<TParam2>();

        internal Action<TParam1, TParam2> ExecuteAction { private get; set; }

        internal bool Match(TParam1 param1, TParam2 param2) => 
            Matcher1.Match(param1) && Matcher2.Match(param2);

        internal void RunActions(TParam1 param1, TParam2 param2)
        {
            baseConfiguration.ThrowAction?.Invoke();
            ExecuteAction?.Invoke(param1, param2);
        }
    }
}
