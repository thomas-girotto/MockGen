using MockGen.Matcher;
using MockGen.Spy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TParam1, TParam2, TReturn> : IMethodSetupReturn<TReturn>
    {
        private Stack<FuncSpecification<TParam1, TParam2, TReturn>> actionByMatchingCriteria = new Stack<FuncSpecification<TParam1, TParam2, TReturn>>();
        private ArgMatcher<TParam1> match1;
        private ArgMatcher<TParam2> match2;

        private MethodSpy<TParam1, TParam2> spy = new MethodSpy<TParam1, TParam2>();

        internal MethodSetupReturn()
        {
            actionByMatchingCriteria.Push(FuncSpecification<TParam1, TParam2, TReturn>.Default);
        }

        public int Calls => spy.GetMatchingCalls(match1, match2).Count();

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
            match1 = ArgMatcher<TParam1>.Create(param1);
            match2 = ArgMatcher<TParam2>.Create(param2);
            return this;
        }

        public void Returns(TReturn value)
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TParam2, TReturn>(match1, match2, (_, _) => value));
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TParam2, TReturn>(match1, match2, (_, _) => throw new TException()));
        }

        public void Throws<TException>(TException exception) where TException : Exception
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TParam2, TReturn>(match1, match2, (_, _) => throw exception));
        }
    }
}
