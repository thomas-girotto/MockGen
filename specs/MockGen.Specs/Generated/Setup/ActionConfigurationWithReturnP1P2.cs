using System;

namespace MockGen.Setup
{
    internal class ActionConfigurationWithReturn<TParam1, TParam2, TReturn>
    {
        private readonly ActionConfiguration<TParam1, TParam2> baseConfiguration;

        public ActionConfigurationWithReturn(ActionConfiguration<TParam1, TParam2> baseConfiguration)
        {
            this.baseConfiguration = baseConfiguration;
        }

        internal bool Match(TParam1 param1, TParam2 param2) => baseConfiguration.Match(param1, param2);

        internal Func<TParam1, TParam2, TReturn> ReturnAction { private get; set; } = (_, _) => default(TReturn);

        internal TReturn RunActions(TParam1 param1, TParam2 param2)
        {
            baseConfiguration.RunActions(param1, param2);
            return ReturnAction(param1, param2);
        }
    }
}
