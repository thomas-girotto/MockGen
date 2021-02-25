using System;

namespace MockGen.Specs.Generated.Helpers
{
    internal interface IMethodSetup
    {
        int Calls { get; }
        void Throws<TException>() where TException : Exception, new();
        void Throws<TException>(TException exception) where TException : Exception;
    }
}
