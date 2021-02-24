using System;
using MockGen.Specs.Generated.Helpers.Matchers;

namespace MockGen.Specs.Generated.Helpers
{
    internal class ActionSpecification<TParam>
    {
        internal static ActionSpecification<TParam> Default = new ActionSpecification<TParam>(new AnyArgMatcher<TParam>(), _ => { });
        private readonly ArgMatcher<TParam> matcher;

        internal ActionSpecification(ArgMatcher<TParam> matcher, Action<TParam> action)
        {
            this.matcher = matcher;
            Action = action;
        }

        internal bool Match(TParam param)
        {
            return matcher.Match(param);
        }

        internal Action<TParam> Action { get; private set; }
    }
}
