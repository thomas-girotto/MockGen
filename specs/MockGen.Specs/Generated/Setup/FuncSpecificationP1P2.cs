using System;
using MockGen.Matcher;

namespace MockGen.Setup
{
    internal class FuncSpecification<TParam1, TParam2, TReturn>
    {
        internal static FuncSpecification<TParam1, TParam2, TReturn> CreateNew()
        {
            return new FuncSpecification<TParam1, TParam2, TReturn>();
        }

        private FuncSpecification() { }

        internal ArgMatcher<TParam1> Matcher1 { get; set; } = new AnyArgMatcher<TParam1>();
        internal ArgMatcher<TParam2> Matcher2 { get; set; } = new AnyArgMatcher<TParam2>();
        internal Func<TParam1, TParam2, TReturn> MockingAction { get; set; } = (_, _) => default(TReturn);
        internal Action<TParam1, TParam2> AdditionalCallback { get; set; } = (_, _) => { };

        internal bool Match(TParam1 param1, TParam2 param2)
        {
            return Matcher1.Match(param1) && Matcher2.Match(param2);
        }

        internal TReturn ExecuteActions(TParam1 param1, TParam2 param2)
        {
            AdditionalCallback(param1, param2);
            return MockingAction(param1, param2);
        }
    }
}
