﻿using System;

namespace MockGen.Setup
{
    internal interface IMethodSetupReturn<TParam1, TParam2, TReturn> : IMethodSetup<TParam1, TParam2>
    {
        IReturnContinuation<TParam1, TParam2> Returns(TReturn value);
        IReturnContinuation<TParam1, TParam2> Returns(Func<TParam1, TParam2, TReturn> returnFunc);
    }

    internal interface IReturnContinuation<TParam1, TParam2>
    {
        void AndExecute(Action<TParam1, TParam2> callback);
    }
}
