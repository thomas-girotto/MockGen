using System;
using MockGen.Specs.Generated.Helpers.Matchers;

namespace MockGen.Specs.Generated.Helpers
{
    internal class FuncSpecification<TParam, TResult>
    {
        internal static FuncSpecification<TParam, TResult> Default = new FuncSpecification<TParam, TResult>(new AnyArgMatcher<TParam>(), _ => default(TResult));
        internal FuncSpecification(ArgMatcher<TParam> matcher, Func<TParam, TResult> action)
        {
            Matcher = matcher;
            Action = action;
        }

        internal ArgMatcher<TParam> Matcher { get; private set; }
        internal Func<TParam, TResult> Action { get; private set; }
    }
}
