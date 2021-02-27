using MockGen.Spy;
using System;

namespace MockGen.Setup
{
    internal class MethodSetupVoid : IMethodSetupVoid
    {
        private Action executeSetupAction = () => { };

        private MethodSpy spy = new MethodSpy();

        public int NumberOfCalls => spy.NumberOfCalls;
        
        public void ExecuteSetup()
        {
            spy.WasCalled();
            executeSetupAction();
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            executeSetupAction = () => throw new TException();
        }

        public void Throws<TException>(TException exception) where TException : Exception
        {
            executeSetupAction = () => throw exception;
        }
    }
}
