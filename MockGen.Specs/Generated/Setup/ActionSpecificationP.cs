using System;
using MockGen.Specs.Generated.Matcher;

namespace MockGen.Specs.Generated.Setup
{
    internal class ActionSpecification<TParam1>
    {
        internal static ActionSpecification<TParam1> Default = new ActionSpecification<TParam1>(new AnyArgMatcher<TParam1>(), (_) => { });
        private readonly ArgMatcher<TParam1> matcher1;

        internal ActionSpecification(ArgMatcher<TParam1> matcher1, Action<TParam1> action)
        {
            this.matcher1 = matcher1;
            Action = action;
        }

        internal bool Match(TParam1 param1)
        {
            return matcher1.Match(param1);
        }

        internal Action<TParam1> Action { get; private set; }
    }
}
