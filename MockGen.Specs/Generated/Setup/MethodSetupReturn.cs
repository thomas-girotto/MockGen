using MockGen.Spy;
using System;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TReturn> : IMethodSetupReturn<TReturn>
    {
        private Func<TReturn> setupAction = () => default(TReturn);
        private MethodSpy spy = new MethodSpy();

        public TReturn ExecuteSetup()
        {
            spy.WasCalled();
            return setupAction();
        }

        public void Returns(TReturn value)
        {
            setupAction = () => value;
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            setupAction = () => throw new TException();
        }

        public void Throws<TException>(TException exception) where TException : Exception
        {
            setupAction = () => throw exception;
        }

        public int Calls => spy.TotalCalls;
    }
}
