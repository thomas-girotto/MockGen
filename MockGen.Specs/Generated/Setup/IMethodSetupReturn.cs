namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TReturn> : IMethodSetup
    {
        void Returns(TReturn value);
    }
}
