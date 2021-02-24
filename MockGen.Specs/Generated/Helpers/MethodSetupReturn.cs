using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Specs.Generated.Helpers
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

        public int Calls => spy.TotalCalls;
    }
}
