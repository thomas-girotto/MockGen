namespace MockGen.Model
{
    public record TypedParameterMethod
    {
        public TypedParameterMethod(int numberOfTypes, bool hasMethodThatReturnsVoid, bool hasMethodThatReturns, bool hasMethodThatReturnsTask)
        {
            NumberOfTypedParameters = numberOfTypes;
            HasMethodThatReturnsVoid = hasMethodThatReturnsVoid;
            HasMethodThatReturns = hasMethodThatReturns;
            HasMethodThatReturnsTask = hasMethodThatReturnsTask;
        }

        public int NumberOfTypedParameters { get; }
        public bool HasMethodThatReturnsVoid { get; }
        public bool HasMethodThatReturns { get; }
        public bool HasMethodThatReturnsTask { get; }
    }
}
