using System;

namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TParam1, TReturn> : IMethodSetup<TParam1> 
    {
        IReturnContinuation<TParam1> Returns(TReturn value);
        IReturnContinuation<TParam1> Returns(Func<TParam1, TReturn> returnFunc);
    }

    internal interface IReturnContinuation<TParam1>
    {
        void AndExecute(Action<TParam1> callback);
    }
}
