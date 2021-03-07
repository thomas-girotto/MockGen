namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TParam1, TParam2, TReturn> : IMethodSetup<TParam1, TParam2>
    {
        IMethodSetup<TParam1, TParam2> Returns(TReturn value);
    }
}
