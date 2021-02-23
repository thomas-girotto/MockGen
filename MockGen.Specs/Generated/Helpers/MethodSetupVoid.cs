using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Throws<TException>() where TException : Exception, new()
        {
            executeSetupAction = () => throw new TException();
        }
    }

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
                if (setupAction.Matcher.Match(param))
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
    }
}
