namespace MockGen.Model
{
    public record TypedParameterMethod
    {
        public TypedParameterMethod(int numberOfTypes, bool hasMethodThatReturnsVoid, bool hasMethodThatReturns)
        {
            NumberOfTypedParameters = numberOfTypes;
            HasMethodThatReturnsVoid = hasMethodThatReturnsVoid;
            HasMethodThatReturns = hasMethodThatReturns;
        }

        public int NumberOfTypedParameters { get; }
        public bool HasMethodThatReturnsVoid { get; }
        public bool HasMethodThatReturns { get; }
    }
}
