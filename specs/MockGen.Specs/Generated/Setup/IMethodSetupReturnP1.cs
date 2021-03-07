namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TParam1, TReturn> : IMethodSetup<TParam1> 
    {
        IMethodSetup<TParam1> Returns(TReturn value);
    }
}
