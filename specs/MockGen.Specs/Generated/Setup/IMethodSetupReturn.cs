using System;

namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TReturn> : IMethodSetup
    {
        IReturnContinuation Returns(TReturn value);
    }

    internal interface IReturnContinuation
    {
        void AndExecute(Action callback);
    }
}
