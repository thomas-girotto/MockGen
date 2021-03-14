namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TParam1, TParam2, TReturn> : IMethodSetup<TParam1, TParam2>
    {
        void Returns(TReturn value);
    }
}
