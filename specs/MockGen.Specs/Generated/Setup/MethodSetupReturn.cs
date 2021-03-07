using System;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TReturn> : MethodSetup, IMethodSetupReturn<TReturn>
    {
        private Func<TReturn> setupAction = () => default(TReturn);

        public void Returns(TReturn value)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Returns));
            setupAction = () => value;
        }

        public override void Throws<TException>()
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Throws));
            setupAction = () => throw new TException();
        }

        public override void Throws<TException>(TException exception)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Throws));
            setupAction = () => throw exception;
        }

        public TReturn ExecuteSetup()
        {
            numberOfCalls++;
            additionalCallback();
            return setupAction();
        }
    }
}
