using MockGen.Matcher;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupVoid<TParam1, TParam2> : MethodSetup<TParam1, TParam2>
    {
        private Stack<ActionSpecification<TParam1, TParam2>> actionByMatchingCriteria = new Stack<ActionSpecification<TParam1, TParam2>>();

        internal MethodSetupVoid()
        {
            actionByMatchingCriteria.Push(ActionSpecification<TParam1, TParam2>.Default);
        }

        public void ExecuteSetup(TParam1 param1, TParam2 param2)
        {
            // Register call with given parameter for future assertions on calls
            calls.Add((param1, param2));
            // Execute the configured action according to given parameters
            foreach (var setupAction in actionByMatchingCriteria)
            {
                if (setupAction.Match(param1, param2))
                {
                    setupAction.Action(param1, param2);
                    return;
                }
            }
        }

        public IMethodSetup<TParam1, TParam2> ForParameter(Arg<TParam1> param1, Arg<TParam2> param2)
        {
            matcher1 = ArgMatcher<TParam1>.Create(param1);
            matcher2 = ArgMatcher<TParam2>.Create(param2);
            return this;
        }

        public override void Throws<TException>()
        {
            actionByMatchingCriteria.Push(new ActionSpecification<TParam1, TParam2>(matcher1, matcher2, (_, _) => throw new TException()));
        }

        public override void Throws<TException>(TException exception)
        {
            actionByMatchingCriteria.Push(new ActionSpecification<TParam1, TParam2>(matcher1, matcher2, (_, _) => throw exception));
        }
    }
}
