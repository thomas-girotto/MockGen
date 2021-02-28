using MockGen.Matcher;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TParam1, TParam2, TReturn> : MethodSetup<TParam1, TParam2>, IMethodSetupReturn<TReturn>
    {
        private Stack<FuncSpecification<TParam1, TParam2, TReturn>> actionByMatchingCriteria = new Stack<FuncSpecification<TParam1, TParam2, TReturn>>();

        internal MethodSetupReturn()
        {
            actionByMatchingCriteria.Push(FuncSpecification<TParam1, TParam2, TReturn>.Default);
        }

        public TReturn ExecuteSetup(TParam1 param1, TParam2 param2)
        {
            // Register call with given parameter for future assertions on calls
            calls.Add((param1, param2));
            // Execute the configured action according to given parameters
            foreach (var setupAction in actionByMatchingCriteria)
            {
                if (setupAction.Match(param1, param2))
                {
                    return setupAction.Action(param1, param2);
                }
            }
            // If we didn't found any setup action to do, execute the default one.
            return FuncSpecification<TParam1, TParam2, TReturn>.Default.Action(param1, param2);
        }

        public IMethodSetupReturn<TReturn> ForParameter(Arg<TParam1> param1, Arg<TParam2> param2)
        {
            matcher1 = ArgMatcher<TParam1>.Create(param1);
            matcher2 = ArgMatcher<TParam2>.Create(param2);
            return this;
        }

        public void Returns(TReturn value)
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TParam2, TReturn>(matcher1, matcher2, (_, _) => value));
        }

        public override void Throws<TException>()
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TParam2, TReturn>(matcher1, matcher2, (_, _) => throw new TException()));
        }

        public override void Throws<TException>(TException exception)
        {
            actionByMatchingCriteria.Push(new FuncSpecification<TParam1, TParam2, TReturn>(matcher1, matcher2, (_, _) => throw exception));
        }
    }
}
