using System;
using MockGen.Specs.Generated.Helpers.Matchers;

namespace MockGen.Specs.Generated.Helpers
{
    internal class ActionSpecification<TParam>
    {
        internal static ActionSpecification<TParam> Default = new ActionSpecification<TParam>(new AnyArgMatcher<TParam>(), _ => { });
        internal ActionSpecification(ArgMatcher<TParam> matcher, Action<TParam> action)
        {
            Matcher = matcher;
            Action = action;
        }

        internal ArgMatcher<TParam> Matcher { get; private set; }
        internal Action<TParam> Action { get; private set; }
    }
}
