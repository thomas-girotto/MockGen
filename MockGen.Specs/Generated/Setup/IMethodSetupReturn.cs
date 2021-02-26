namespace MockGen.Setup
{
    interface IMethodSetupReturn<TReturn> : IMethodSetup
    {
        void Returns(TReturn value);
    }
}
