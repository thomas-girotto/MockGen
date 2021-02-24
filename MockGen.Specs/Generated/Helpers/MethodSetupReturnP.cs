using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Specs.Generated.Helpers
{
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
}
