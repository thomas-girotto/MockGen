using System;
using MockGen.Matcher;

namespace MockGen.Setup
{
    internal class ActionSpecification<TParam1, TParam2>
    {
        internal static ActionSpecification<TParam1, TParam2> CreateNew()
        {
            return new ActionSpecification<TParam1, TParam2>();
        }

        private ActionSpecification() { }

        internal ArgMatcher<TParam1> Matcher1 { get; set; } = new AnyArgMatcher<TParam1>();
        internal ArgMatcher<TParam2> Matcher2 { get; set; } = new AnyArgMatcher<TParam2>();
        internal Action<TParam1, TParam2> MockingAction { get; set; } = (_, _) => { };
        internal Action<TParam1, TParam2> AdditionalCallback { get; set; } = (_, _) => { };

        internal bool Match(TParam1 param1, TParam2 param2)
        {
            return Matcher1.Match(param1) && Matcher2.Match(param2);
        }

        internal void ExecuteActions(TParam1 param1, TParam2 param2)
        {
            AdditionalCallback(param1, param2);
            MockingAction(param1, param2);
        }
    }
}
