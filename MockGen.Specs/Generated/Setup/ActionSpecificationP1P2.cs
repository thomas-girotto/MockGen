using System;
using MockGen.Matcher;

namespace MockGen.Setup
{
    internal class ActionSpecification<TParam1, TParam2>
    {
        private readonly ArgMatcher<TParam1> matcher1;
        private readonly ArgMatcher<TParam2> matcher2;

        internal static ActionSpecification<TParam1, TParam2> Default = new ActionSpecification<TParam1, TParam2>(
            new AnyArgMatcher<TParam1>(), 
            new AnyArgMatcher<TParam2>(), 
            (_, _) => { });

        internal ActionSpecification(ArgMatcher<TParam1> matcher1, ArgMatcher<TParam2> matcher2, Action<TParam1, TParam2> action)
        {
            this.matcher1 = matcher1;
            this.matcher2 = matcher2;
            Action = action;
        }

        internal bool Match(TParam1 param1, TParam2 param2)
        {
            return matcher1.Match(param1) && matcher2.Match(param2);
        }

        internal Action<TParam1, TParam2> Action { get; private set; }
    }
}
