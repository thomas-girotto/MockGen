using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Specs.Generated.Helpers
{
    interface IMethodSetupReturn<TReturn> : IMethodSetup
    {
        void Returns(TReturn value);
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

        public void Returns(TReturn value)
        {
            setupAction = () => value;
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            setupAction = () => throw new TException();
        }

        public int Calls => spy.TotalCalls;
    }

    internal class MethodSetupReturn<TParam, TReturn> : IMethodSetupReturn<TReturn>
    {
        private Stack<FuncSpecification<TParam, TReturn>> actionByMatchingCriteria = new Stack<FuncSpecification<TParam, TReturn>>();
        private ArgMatcher<TParam> matchParameter;
        private MethodSpy<TParam> spy = new MethodSpy<TParam>();

        public MethodSetupReturn()
        {
            actionByMatchingCriteria.Push(FuncSpecification<TParam, TReturn>.Default);
        }

        public IMethodSetupReturn<TReturn> ForParameter(Arg<TParam> paramValue)
        {
            matchParameter = ArgMatcher<TParam>.Create(paramValue);
            return this;
        }

        public IMethodSetupReturn<TReturn> ForParameter(Predicate<TParam> matchingPredicate)
        {
            matchParameter = ArgMatcher<TParam>.Create(matchingPredicate);
            return this;
        }

        public void Returns(TReturn value)
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam, TReturn>(matchParameter, (_) => value));
        }

        public TReturn ExecuteSetup(TParam param)
        {
            spy.RegisterCallParameters(param);
            foreach (var setupAction in actionByMatchingCriteria)
            {
                if (setupAction.Matcher.Match(param))
                {
                    return setupAction.Action(param);
                }
            }

            return FuncSpecification<TParam, TReturn>.Default.Action(param);
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam, TReturn>(matchParameter, (_) => throw new TException()));
        }

        public int Calls => spy.GetMatchingCalls(matchParameter).Count();
    }

    internal class MethodSetupReturn<TParam1, TParam2, TReturn> : IMethodSetupReturn<TReturn>
    {
        private Stack<FuncSpecification<TParam1, TParam2, TReturn>> actionByMatchingCriteria = new Stack<FuncSpecification<TParam1, TParam2, TReturn>>();
        private ArgMatcher<TParam1> matchParameter1;
        private ArgMatcher<TParam2> matchParameter2;

        private MethodSpy<TParam1, TParam2> spy = new MethodSpy<TParam1, TParam2>();

        internal MethodSetupReturn()
        {
            actionByMatchingCriteria.Push(FuncSpecification<TParam1, TParam2, TReturn>.Default);
        }

        public int Calls => spy.GetMatchingCalls(matchParameter1, matchParameter2).Count();

        public TReturn ExecuteSetup(TParam1 param1, TParam2 param2)
        {
            spy.RegisterCallParameters(param1, param2);
            foreach (var setupAction in actionByMatchingCriteria)
            {
                if (setupAction.Match(param1, param2))
                {
                    return setupAction.Action(param1, param2);
                }
            }
            return FuncSpecification<TParam1, TParam2, TReturn>.Default.Action(param1, param2);
        }

        public IMethodSetupReturn<TReturn> ForParameter(Arg<TParam1> param1, Arg<TParam2> param2)
        {
            matchParameter1 = ArgMatcher<TParam1>.Create(param1);
            matchParameter2 = ArgMatcher<TParam2>.Create(param2);
            return this;
        }

        public void Returns(TReturn value)
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TParam2, TReturn>(matchParameter1, matchParameter2, (_, _) => value));
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TParam2, TReturn>(matchParameter1, matchParameter2, (_, _) => throw new TException()));
        }
    }
}
