using MockGen.Matcher;
using System;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupVoid<TParam1, TParam2> : MethodSetup<TParam1, TParam2>
    {
        private Stack<ActionSpecification<TParam1, TParam2>> actionByMatchingCriteria = new Stack<ActionSpecification<TParam1, TParam2>>();
        private ActionSpecification<TParam1, TParam2> currentlyConfiguredAction = ActionSpecification<TParam1, TParam2>.CreateNew();

        public IMethodSetup<TParam1, TParam2> ForParameter(Arg<TParam1> param1, Arg<TParam2> param2)
        {
            if (!setupDone)
            {
                actionByMatchingCriteria.Push(currentlyConfiguredAction);
                currentlyConfiguredAction = ActionSpecification<TParam1, TParam2>.CreateNew();

                currentlyConfiguredAction.Matcher1 = ArgMatcher<TParam1>.Create(param1);
                currentlyConfiguredAction.Matcher2 = ArgMatcher<TParam2>.Create(param2);
            }
            else
            {
                matcher1 = ArgMatcher<TParam1>.Create(param1);
                matcher2 = ArgMatcher<TParam2>.Create(param2);
            }

            return this;
        }

        public override void Throws<TException>()
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Throws));
            currentlyConfiguredAction.MockingAction = (_, _) => throw new TException();
        }

        public override void Throws<TException>(TException exception)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Throws));
            currentlyConfiguredAction.MockingAction = (_, _) => throw exception;
        }

        public override void Execute(Action<TParam1, TParam2> callback)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Execute));
            currentlyConfiguredAction.AdditionalCallback = callback;
        }

        internal override void SetupDone()
        {
            base.SetupDone();
            actionByMatchingCriteria.Push(currentlyConfiguredAction);
            currentlyConfiguredAction = ActionSpecification<TParam1, TParam2>.CreateNew();
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
                    setupAction.ExecuteActions(param1, param2);
                    return;
                }
            }
        }

    }
}
