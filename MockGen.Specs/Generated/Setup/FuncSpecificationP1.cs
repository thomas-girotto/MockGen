using System;
using MockGen.Matcher;

namespace MockGen.Setup
{
    internal class FuncSpecification<TParam1, TReturn>
    {
        internal static FuncSpecification<TParam1, TReturn> CreateNew()
        {
            return new FuncSpecification<TParam1, TReturn>();
        }

        private FuncSpecification() { }

        internal ArgMatcher<TParam1> Matcher1 { get; set; } = new AnyArgMatcher<TParam1>();
        internal Func<TParam1, TReturn> MockingAction { get; set; } = (_) => default(TReturn);
        internal Action<TParam1> AdditionalCallback { get; set; } = (_) => { };

        internal bool Match(TParam1 param1)
        {
            return Matcher1.Match(param1);
        }

        internal TReturn ExecuteActions(TParam1 param1)
        {
            AdditionalCallback(param1);
            return MockingAction(param1);
        }
    }
}
