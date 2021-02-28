using MockGen.Matcher;
using System;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TParam1, TReturn> : MethodSetup<TParam1>, IMethodSetupReturn<TReturn>
    {
        private Stack<FuncSpecification<TParam1, TReturn>> actionByMatchingCriteria = new Stack<FuncSpecification<TParam1, TReturn>>();

        public MethodSetupReturn()
        {
            actionByMatchingCriteria.Push(FuncSpecification<TParam1, TReturn>.Default);
        }

        public IMethodSetupReturn<TReturn> ForParameter(Arg<TParam1> paramValue)
        {
            matcher = ArgMatcher<TParam1>.Create(paramValue);
            return this;
        }

        public IMethodSetupReturn<TReturn> ForParameter(Predicate<TParam1> matchingPredicate)
        {
            matcher = ArgMatcher<TParam1>.Create(matchingPredicate);
            return this;
        }

        public void Returns(TReturn value)
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TReturn>(matcher, (_) => value));
        }

        public TReturn ExecuteSetup(TParam1 param)
        {
            calls.Add(param);
            foreach (var setupAction in actionByMatchingCriteria)
            {
                if (setupAction.Match(param))
                {
                    return setupAction.Action(param);
                }
            }

            return FuncSpecification<TParam1, TReturn>.Default.Action(param);
        }

        public override void Throws<TException>()
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TReturn>(matcher, (_) => throw new TException()));
        }

        public override void Throws<TException>(TException exception)
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TReturn>(matcher, (_) => throw exception));
        }
    }
}
