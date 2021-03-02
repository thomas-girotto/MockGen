using MockGen.Matcher;
using System;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupVoid<TParam> : MethodSetup<TParam>
    {
        private Stack<ActionSpecification<TParam>> actionByMatchingCriteria = new Stack<ActionSpecification<TParam>>();
        private ActionSpecification<TParam> currentlyConfiguredAction = ActionSpecification<TParam>.CreateNew();

        public IMethodSetup<TParam> ForParameter(Arg<TParam> paramValue)
        {
            if (!setupDone)
            {
                actionByMatchingCriteria.Push(currentlyConfiguredAction);
                currentlyConfiguredAction = ActionSpecification<TParam>.CreateNew();
                currentlyConfiguredAction.Matcher1 = ArgMatcher<TParam>.Create(paramValue);
            }
            else
            {
                matcher = ArgMatcher<TParam>.Create(paramValue);
            }

            return this;
        }

        public override void Throws<TException>()
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Throws));
            currentlyConfiguredAction.MockingAction = (_) => throw new TException();
        }

        public override void Throws<TException>(TException exception)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Throws));
            currentlyConfiguredAction.MockingAction = (_) => throw exception;
        }

        public override void Execute(Action<TParam> callback)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Execute));
            currentlyConfiguredAction.AdditionalCallback = callback;
        }

        internal override void SetupDone()
        {
            base.SetupDone();
            actionByMatchingCriteria.Push(currentlyConfiguredAction);
            currentlyConfiguredAction = ActionSpecification<TParam>.CreateNew();
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
                    setupAction.ExecuteActions(param);
                    return;
                }
            }
        }
    }
}
