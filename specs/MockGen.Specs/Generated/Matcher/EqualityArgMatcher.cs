namespace MockGen.Matcher
{
    internal class EqualityArgMatcher<TParam> : ArgMatcher<TParam>
    {
        private readonly TParam objectToMatchWith;

        public EqualityArgMatcher(TParam objectToMatchWith)
        {
            this.objectToMatchWith = objectToMatchWith;
        }

        internal override bool Match(TParam param)
        {
            return (param == null && objectToMatchWith == null) || (param != null && param.Equals(objectToMatchWith));
        }
    }
}
