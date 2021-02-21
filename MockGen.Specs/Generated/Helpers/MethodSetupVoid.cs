using System;
using System.Collections.Generic;

namespace MockGen.Specs.Generated.Helpers
{
    interface IMethodSetupVoid : IMethodSetup { }

    internal class MethodSetupVoid : IMethodSetupVoid
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

    internal class MethodSetupVoid<TParam> : IMethodSetupVoid
    {
        private Arg<TParam> parameterValue = Arg<TParam>.Any;
        Dictionary<Arg<TParam>, Action<TParam>> setupActionByParam = new Dictionary<Arg<TParam>, Action<TParam>>
        {
            { Arg<TParam>.Any, (_) => { } }
        };

        private MethodSpy<TParam> spy = new MethodSpy<TParam>();

        public int Calls => spy.GetCallsFor(parameterValue);

        public void ExecuteSetup(TParam param)
        {
            var arg = Arg<TParam>.Create(param);
            spy.WasCalled(arg);
            if (setupActionByParam.ContainsKey(arg))
            {
                setupActionByParam[arg](param);
            }
            else
            {
                setupActionByParam[Arg<TParam>.Any](param);
            }
        }

        public IMethodSetupVoid ForParameter(Arg<TParam> paramValue)
        {
            parameterValue = paramValue;
            return this;
        }

        public void WillThrow<TException>() where TException : Exception, new()
        {
            setupActionByParam[parameterValue] = (_) => throw new TException();
        }
    }
}
