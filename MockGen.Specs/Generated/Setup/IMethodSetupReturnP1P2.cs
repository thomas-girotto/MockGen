namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TParam1, TParam2, TReturn> : IMethodSetupReturn<TReturn>, IMethodSetup<TParam1, TParam2> { }

}
