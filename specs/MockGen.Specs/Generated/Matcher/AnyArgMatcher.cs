namespace MockGen.Matcher
{
    internal class AnyArgMatcher<TParam> : ArgMatcher<TParam>
    {
        internal override bool Match(TParam param)
        {
            return true;
        }
    }
}
