namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TParam1, TReturn> : IMethodSetupReturn<TReturn>, IMethodSetup<TParam1> { }
}
