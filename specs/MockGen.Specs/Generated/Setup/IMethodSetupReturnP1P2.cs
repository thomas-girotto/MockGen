using System;

namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TParam1, TParam2, TReturn> : IMethodSetup<TParam1, TParam2>
    {
        IReturnContinuation<TParam1, TParam2> Returns(TReturn value);
    }

    internal interface IReturnContinuation<TParam1, TParam2>
    {
        void AndExecute(Action<TParam1, TParam2> callback);
    }
}
