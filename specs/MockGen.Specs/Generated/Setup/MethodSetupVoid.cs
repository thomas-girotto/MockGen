using System;

namespace MockGen.Setup
{
    internal class MethodSetupVoid : MethodSetup, IMethodSetup
    {
        private Action executeSetupAction = () => { };
        
        public override void Throws<TException>()
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Throws));
            executeSetupAction = () => throw new TException();
        }

        public override void Throws<TException>(TException exception)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Throws));
            executeSetupAction = () => throw exception;
        }

        public void ExecuteSetup()
        {
            numberOfCalls++;
            additionalCallback();
            executeSetupAction();
        }
    }
}
