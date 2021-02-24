using System;

namespace MockGen.Specs.Generated.Helpers
{
    internal class MethodSetupVoid : IMethodSetupVoid
    {
        private Action executeSetupAction = () => { };

        private MethodSpy spy = new MethodSpy();

        public int Calls => spy.TotalCalls;
        
        public void ExecuteSetup()
        {
            spy.WasCalled();
            executeSetupAction();
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            executeSetupAction = () => throw new TException();
        }
    }
}
