using System;

namespace MockGen.Setup
{
    internal interface IMethodSetupBase
    {
        int NumberOfCalls { get; }
        void Throws<TException>() where TException : Exception, new();
        void Throws<TException>(TException exception) where TException : Exception;
    }
}
