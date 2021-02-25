using MockGen.Specs.Generated.Matcher;
using MockGen.Specs.Generated.Spy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Specs.Generated.Setup
{
    internal class MethodSetupVoid<TParam1, TParam2> : IMethodSetupVoid
    {
        private Stack<ActionSpecification<TParam1, TParam2>> actionByMatchingCriteria = new Stack<ActionSpecification<TParam1, TParam2>>();
        private ArgMatcher<TParam1> matchParameter1;
        private ArgMatcher<TParam2> matchParameter2;

        private MethodSpy<TParam1, TParam2> spy = new MethodSpy<TParam1, TParam2>();

        internal MethodSetupVoid()
        {
            actionByMatchingCriteria.Push(ActionSpecification<TParam1, TParam2>.Default);
        }

        public int Calls => spy.GetMatchingCalls(matchParameter1, matchParameter2).Count();

        public void ExecuteSetup(TParam1 param1, TParam2 param2)
        {
            spy.RegisterCallParameters(param1, param2);
            foreach (var setupAction in actionByMatchingCriteria)
            {
                if (setupAction.Match(param1, param2))
                {
                    setupAction.Action(param1, param2);
                }
            }
        }

        public IMethodSetupVoid ForParameter(Arg<TParam1> param1, Arg<TParam2> param2)
        {
            matchParameter1 = ArgMatcher<TParam1>.Create(param1);
            matchParameter2 = ArgMatcher<TParam2>.Create(param2);
            return this;
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            actionByMatchingCriteria.Push(new ActionSpecification<TParam1, TParam2>(matchParameter1, matchParameter2, (_, _) => throw new TException()));
        }

        public void Throws<TException>(TException exception) where TException : Exception
        {
            actionByMatchingCriteria.Push(new ActionSpecification<TParam1, TParam2>(matchParameter1, matchParameter2, (_, _) => throw exception));
        }
    }
}
