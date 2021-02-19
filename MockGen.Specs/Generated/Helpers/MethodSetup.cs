﻿using System;
using System.Collections.Generic;

namespace MockGen.Specs.Generated.Helpers
{
    interface IMethodSetup
    {
        int Calls { get; }
        void WillThrow<TException>() where TException : Exception, new();
    }

    internal class MethodSetup : IMethodSetup
    {
        private Action executeSetupAction = () => { };

        private MethodSpy spy = new MethodSpy();

        public int Calls => spy.TotalCalls;
        
        public void ExecuteSetup()
        {
            spy.WasCalled();
            executeSetupAction();
        }

        public void WillThrow<TException>() where TException : Exception, new()
        {
            executeSetupAction = () => throw new TException();
        }
    }

    interface IMethodSetup<TReturn> : IMethodSetup
    {
        void WillReturn(TReturn value);
    }

    internal class MethodSetup<TReturn> : IMethodSetup<TReturn>
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

    interface IMethodSetup<TReturn, TParam> : IMethodSetup<TReturn>
    {
    }

    internal class MethodSetup<TReturn, TParam> : IMethodSetup<TReturn, TParam>
    {
        private Arg<TParam> parameterValue = Arg<TParam>.Any;
        private MethodSpy<TParam> spy = new MethodSpy<TParam>();
        Dictionary<Arg<TParam>, Func<TParam, TReturn>> setupActionByParam = new Dictionary<Arg<TParam>, Func<TParam, TReturn>>
        {
            { Arg<TParam>.Any, _ => default(TReturn) }
        };

        public MethodSetup<TReturn, TParam> ForParameter(Arg<TParam> paramValue)
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
            spy.WasCalled(param);
            var arg = new Arg<TParam>(param);
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
