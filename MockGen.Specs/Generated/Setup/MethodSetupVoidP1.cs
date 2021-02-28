using MockGen.Matcher;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupVoid<TParam> : MethodSetup<TParam>
    {
        private Stack<ActionSpecification<TParam>> actionByMatchingCriteria = new Stack<ActionSpecification<TParam>>();

        internal MethodSetupVoid()
        {
            actionByMatchingCriteria.Push(ActionSpecification<TParam>.Default);
        }

        public void ExecuteSetup(TParam param)
        {
            // Register call with given parameter for future assertions on calls
            calls.Add(param);
            // Execute the configured action according to given parameters
            foreach (var setupAction in actionByMatchingCriteria)
            {
                if (setupAction.Match(param))
                {
                    setupAction.Action(param);
                    return;
                }
            }
        }

        public IMethodSetup<TParam> ForParameter(Arg<TParam> paramValue)
        {
            matcher = ArgMatcher<TParam>.Create(paramValue);
            return this;
        }

        public override void Throws<TException>()
        {
            actionByMatchingCriteria.Push(new ActionSpecification<TParam>(matcher, (_) => throw new TException()));
        }

        public override void Throws<TException>(TException exception)
        {
            actionByMatchingCriteria.Push(new ActionSpecification<TParam>(matcher, (_) => throw exception));
        }
    }
}
