using MockGen.Matcher;
using System;
using System.Collections.Generic;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TParam1, TReturn> : MethodSetup<TParam1>, IMethodSetupReturn<TParam1, TReturn>
    {
        private Stack<FuncSpecification<TParam1, TReturn>> actionByMatchingCriteria = new Stack<FuncSpecification<TParam1, TReturn>>();
        private FuncSpecification<TParam1, TReturn> currentlyConfiguredAction = FuncSpecification<TParam1, TReturn>.CreateNew();

        public IMethodSetupReturn<TParam1, TReturn> ForParameter(Arg<TParam1> paramValue)
        {
            if (!setupDone)
            {
                actionByMatchingCriteria.Push(currentlyConfiguredAction);
                currentlyConfiguredAction = FuncSpecification<TParam1, TReturn>.CreateNew();
                currentlyConfiguredAction.Matcher1 = ArgMatcher<TParam1>.Create(paramValue);
            }
            else
            {
                matcher = ArgMatcher<TParam1>.Create(paramValue);
            }

            return this;
        }

        public IMethodSetup<TParam1> Returns(TReturn value)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Returns));
            currentlyConfiguredAction.MockingAction = (_) => value;
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

        public override void Execute(Action<TParam1> callback)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Execute));
            currentlyConfiguredAction.AdditionalCallback = callback;
        }

        internal override void SetupDone()
        {
            base.SetupDone();
            actionByMatchingCriteria.Push(currentlyConfiguredAction);
            currentlyConfiguredAction = FuncSpecification<TParam1, TReturn>.CreateNew();
        }

        public TReturn ExecuteSetup(TParam1 param)
        {
            calls.Add(param);
            foreach (var setupAction in actionByMatchingCriteria)
            {
                if (setupAction.Match(param))
                {
                    return setupAction.ExecuteActions(param);
                }
            }

            return default(TReturn);
        }
    }
}
