using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Specs.Generated.Helpers
{
    internal class MethodSetupReturn<TParam1, TReturn> : IMethodSetupReturn<TReturn>
    {
        private Stack<FuncSpecification<TParam1, TReturn>> actionByMatchingCriteria = new Stack<FuncSpecification<TParam1, TReturn>>();
        private ArgMatcher<TParam1> match1;
        private MethodSpy<TParam1> spy = new MethodSpy<TParam1>();

        public MethodSetupReturn()
        {
            actionByMatchingCriteria.Push(FuncSpecification<TParam1, TReturn>.Default);
        }

        public IMethodSetupReturn<TReturn> ForParameter(Arg<TParam1> paramValue)
        {
            match1 = ArgMatcher<TParam1>.Create(paramValue);
            return this;
        }

        public IMethodSetupReturn<TReturn> ForParameter(Predicate<TParam1> matchingPredicate)
        {
            match1 = ArgMatcher<TParam1>.Create(matchingPredicate);
            return this;
        }

        public void Returns(TReturn value)
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TReturn>(match1, (_) => value));
        }

        public TReturn ExecuteSetup(TParam1 param)
        {
            spy.RegisterCallParameters(param);
            foreach (var setupAction in actionByMatchingCriteria)
            {
                if (setupAction.Matcher.Match(param))
                {
                    return setupAction.Action(param);
                }
            }

            return FuncSpecification<TParam1, TReturn>.Default.Action(param);
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TReturn>(match1, (_) => throw new TException()));
        }

        public int Calls => spy.GetMatchingCalls(match1).Count();
    }
}
