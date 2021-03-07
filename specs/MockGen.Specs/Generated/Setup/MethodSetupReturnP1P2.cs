using MockGen.Matcher;
using System;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TParam1, TParam2, TReturn> : MethodSetup<TParam1, TParam2>, IMethodSetupReturn<TParam1, TParam2, TReturn>
    {
        private Stack<FuncSpecification<TParam1, TParam2, TReturn>> actionByMatchingCriteria = new Stack<FuncSpecification<TParam1, TParam2, TReturn>>();
        private FuncSpecification<TParam1, TParam2, TReturn> currentlyConfiguredAction = FuncSpecification<TParam1, TParam2, TReturn>.CreateNew();

        public IMethodSetupReturn<TParam1, TParam2, TReturn> ForParameter(Arg<TParam1> param1, Arg<TParam2> param2)
        {
            if (!setupDone)
            {
                actionByMatchingCriteria.Push(currentlyConfiguredAction);
                currentlyConfiguredAction = FuncSpecification<TParam1, TParam2, TReturn>.CreateNew();

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

        public IMethodSetup<TParam1, TParam2> Returns(TReturn value)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Returns));
            currentlyConfiguredAction.MockingAction = (_, _) => value;
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
            currentlyConfiguredAction = FuncSpecification<TParam1, TParam2, TReturn>.CreateNew();
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
                    return setupAction.ExecuteActions(param1, param2);
                }
            }
            // If we didn't found any setup action to do, execute the default one.
            return default(TReturn);
        }
    }
}
