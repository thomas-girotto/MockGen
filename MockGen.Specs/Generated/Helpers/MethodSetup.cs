using System.Collections.Generic;

namespace MockGen.Specs.Generated.Helpers
{
    interface IMethodSetup<TReturn>
    {
        int Calls { get; }
        void WillReturn(TReturn value);
    }

    internal class MethodSetup<TReturn> : IMethodSetup<TReturn>
    {
        private TReturn value = default(TReturn);

        public MethodSpy Spy { get; private set; } = new MethodSpy();

        public TReturn GetValue()
        {
            return value;
        }

        public void WillReturn(TReturn value)
        {
            this.value = value;
        }

        public int Calls => Spy.TotalCalls;
    }

    interface IMethodSetup<TReturn, TParam> : IMethodSetup<TReturn>
    {
    }

    internal class MethodSetup<TReturn, TParam> : IMethodSetup<TReturn, TParam>
    {
        private Dictionary<Arg<TParam>, TReturn> valueToReturnByParam = new Dictionary<Arg<TParam>, TReturn>() 
        {
            { Arg<TParam>.Any, default(TReturn) }
        };

        private Arg<TParam> parameterValue = Arg<TParam>.Any;

        public MethodSpy<TParam> Spy { get; } = new MethodSpy<TParam>();

        public MethodSetup<TReturn, TParam> ForParameter(Arg<TParam> paramValue)
        {
            parameterValue = paramValue;
            return this;
        }

        public void WillReturn(TReturn value)
        {
            valueToReturnByParam[parameterValue] = value;
            parameterValue = Arg<TParam>.Any;
        }

        public TReturn GetValue(TParam param)
        {
            var arg = new Arg<TParam>(param);
            return valueToReturnByParam.ContainsKey(arg)
                ? valueToReturnByParam[arg]
                : valueToReturnByParam[Arg<TParam>.Any];
        }

        public int Calls => Spy.GetCallsFor(parameterValue);
    }
}
