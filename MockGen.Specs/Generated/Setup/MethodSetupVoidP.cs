using MockGen.Specs.Generated.Matcher;
using MockGen.Specs.Generated.Spy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Specs.Generated.Setup
{
    internal class MethodSetupVoid<TParam> : IMethodSetupVoid
    {
        private Stack<ActionSpecification<TParam>> actionByMatchingCriteria = new Stack<ActionSpecification<TParam>>();
        private ArgMatcher<TParam> matchParameter;

        private MethodSpy<TParam> spy = new MethodSpy<TParam>();

        internal MethodSetupVoid()
        {
            actionByMatchingCriteria.Push(ActionSpecification<TParam>.Default);
        }

        public int Calls => spy.GetMatchingCalls(matchParameter).Count();

        public void ExecuteSetup(TParam param)
        {
            spy.RegisterCallParameters(param);
            foreach (var setupAction in actionByMatchingCriteria)
            {
                if (setupAction.Match(param))
                {
                    setupAction.Action(param);
                }
            }
        }

        public IMethodSetupVoid ForParameter(Arg<TParam> paramValue)
        {
            matchParameter = ArgMatcher<TParam>.Create(paramValue);
            return this;
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            actionByMatchingCriteria.Push(new ActionSpecification<TParam>(matchParameter, (_) => throw new TException()));
        }

        public void Throws<TException>(TException exception) where TException : Exception
        {
            actionByMatchingCriteria.Push(new ActionSpecification<TParam>(matchParameter, (_) => throw exception));
        }
    }
}
