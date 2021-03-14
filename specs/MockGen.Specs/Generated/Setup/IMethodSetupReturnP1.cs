namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TParam1, TReturn> : IMethodSetup<TParam1> 
    {
        void Returns(TReturn value);
    }
}
