using System;

namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TParam1, TReturn> : IMethodSetup<TParam1> 
    {
        IReturnContinuation<TParam1> Returns(TReturn value);
    }

    internal interface IReturnContinuation<TParam1>
    {
        void AndExecute(Action<TParam1> callback);
    }
}
