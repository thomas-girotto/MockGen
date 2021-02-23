using System;
using MockGen.Specs.Generated.Helpers.Matchers;

namespace MockGen.Specs.Generated.Helpers
{
    internal abstract class ArgMatcher<TParam>
    {
        internal abstract bool Match(TParam param);

        internal static ArgMatcher<TParam> Create(Arg<TParam> arg)
        {
            if (ReferenceEquals(arg, Arg<TParam>.Any))
            {
                return new AnyArgMatcher<TParam>();
            }
            if (arg.Predicate == null)
            {
                return new EqualityArgMatcher<TParam>(arg.Value);
            }
            return new PredicateArgMatcher<TParam>(arg.Predicate);
        }

        internal static ArgMatcher<TParam> Create(Predicate<TParam> predicate)
        {
            return new PredicateArgMatcher<TParam>(predicate);
        }
    }
}
