﻿using System;
using System.Collections.Generic;

namespace MockGen.Specs.Generated.Helpers
{
    interface IMethodSetupReturn<TReturn> : IMethodSetup
    {
        void WillReturn(TReturn value);
    }


    internal class MethodSetupReturn<TReturn> : IMethodSetupReturn<TReturn>
    {
        private Func<TReturn> setupAction = () => default(TReturn);
        private MethodSpy spy = new MethodSpy();

        public TReturn ExecuteSetup()
        {
            spy.WasCalled();
            return setupAction();
        }

        public void WillReturn(TReturn value)
        {
            setupAction = () => value;
        }

        public void WillThrow<TException>() where TException : Exception, new()
        {
            setupAction = () => throw new TException();
        }

        public int Calls => spy.TotalCalls;
    }

    internal class MethodSetupReturn<TParam, TReturn> : IMethodSetupReturn<TReturn>
    {
        private Arg<TParam> parameterValue = Arg<TParam>.Any;
        private MethodSpy<TParam> spy = new MethodSpy<TParam>();
        Dictionary<Arg<TParam>, Func<TParam, TReturn>> setupActionByParam = new Dictionary<Arg<TParam>, Func<TParam, TReturn>>
        {
            { Arg<TParam>.Any, _ => default(TReturn) }
        };

        public MethodSetupReturn<TParam, TReturn> ForParameter(Arg<TParam> paramValue)
        {
            parameterValue = paramValue;
            return this;
        }

        public void WillReturn(TReturn value)
        {
            setupActionByParam[parameterValue] = (_) => value;
            parameterValue = Arg<TParam>.Any;
        }

        public TReturn ExecuteSetup(TParam param)
        {
            var arg = Arg<TParam>.Create(param);
            spy.WasCalled(arg);
            return setupActionByParam.ContainsKey(arg)
                ? setupActionByParam[arg](param)
                : setupActionByParam[Arg<TParam>.Any](param);
        }

        public void WillThrow<TException>() where TException : Exception, new()
        {
            setupActionByParam[parameterValue] = (_) => throw new TException();
            parameterValue = Arg<TParam>.Any;
        }

        public int Calls => spy.GetCallsFor(parameterValue);
    }
}
