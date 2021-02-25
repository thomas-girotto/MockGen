using System;

namespace MockGen.Specs.Generated.Matcher
{
    internal class PredicateArgMatcher<TParam> : ArgMatcher<TParam>
    {
        private readonly Predicate<TParam> predicate;

        public PredicateArgMatcher(Predicate<TParam> predicate)
        {
            this.predicate = predicate;
        }

        internal override bool Match(TParam param)
        {
            return predicate(param);
        }
    }
}
