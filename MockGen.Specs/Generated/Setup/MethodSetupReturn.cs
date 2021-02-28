using System;

namespace MockGen.Setup
{
    internal class MethodSetupReturn<TReturn> : IMethodSetupReturn<TReturn>
    {
        private Func<TReturn> setupAction = () => default(TReturn);
        private int numberOfCalls;

        public TReturn ExecuteSetup()
        {
            numberOfCalls++;
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

        public int NumberOfCalls => numberOfCalls;
    }
}
