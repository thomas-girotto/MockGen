using System;

namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TReturn> : IMethodSetup
    {
        IReturnContinuation Returns(TReturn value);
        IReturnContinuation Returns(Func<TReturn> returnFunc);
    }

    internal interface IReturnContinuation
    {
        void AndExecute(Action callback);
    }
}
