using System;

namespace MockGen.Specs.Generated.Helpers
{
    internal interface IMethodSetup
    {
        int Calls { get; }
        void WillThrow<TException>() where TException : Exception, new();
    }
}
