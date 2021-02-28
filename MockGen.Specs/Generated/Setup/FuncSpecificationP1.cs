using System;
using MockGen.Matcher;

namespace MockGen.Setup
{
    internal class FuncSpecification<TParam, TResult>
    {
        private readonly ArgMatcher<TParam> matcher1;
        
        internal static FuncSpecification<TParam, TResult> Default = new FuncSpecification<TParam, TResult>(new AnyArgMatcher<TParam>(), _ => default(TResult));

        internal FuncSpecification(ArgMatcher<TParam> matcher, Func<TParam, TResult> action)
        {
            matcher1 = matcher;
            Action = action;
        }

        internal bool Match(TParam param1)
        {
            return matcher1.Match(param1);
        }

        internal Func<TParam, TResult> Action { get; private set; }
    }
}
