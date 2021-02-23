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

    internal class FuncSpecification<TParam1, TParam2, TReturn>
    {
        private readonly ArgMatcher<TParam1> matcher1;
        private readonly ArgMatcher<TParam2> matcher2;

        internal static FuncSpecification<TParam1, TParam2, TReturn> Default = new FuncSpecification<TParam1, TParam2, TReturn>(new AnyArgMatcher<TParam1>(), new AnyArgMatcher<TParam2>(), (_, _) => default(TReturn));

        internal FuncSpecification(ArgMatcher<TParam1> matcher1, ArgMatcher<TParam2> matcher2, Func<TParam1, TParam2, TReturn> action)
        {
            this.matcher1 = matcher1;
            this.matcher2 = matcher2;
            Action = action;
        }

        internal bool Match(TParam1 param1, TParam2 param2)
        {
            return matcher1.Match(param1) && matcher2.Match(param2);
        }

        internal Func<TParam1, TParam2, TReturn> Action { get; private set; }

    }
}
