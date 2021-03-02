using System;
using MockGen.Matcher;

namespace MockGen.Setup
{
    internal class ActionSpecification<TParam1>
    {
        internal static ActionSpecification<TParam1> CreateNew()
        {
            return new ActionSpecification<TParam1>();
        }

        private ActionSpecification() { }

        internal ArgMatcher<TParam1> Matcher1 { get; set; } = new AnyArgMatcher<TParam1>();
        internal Action<TParam1> MockingAction { get; set; } = (_) => { };
        internal Action<TParam1> AdditionalCallback { get; set; } = (_) => { };

        internal bool Match(TParam1 param1)
        {
            return Matcher1.Match(param1);
        }

        internal void ExecuteActions(TParam1 param1)
        {
            AdditionalCallback(param1);
            MockingAction(param1);
        }
    }
}
