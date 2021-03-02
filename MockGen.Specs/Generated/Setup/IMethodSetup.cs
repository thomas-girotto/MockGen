using System;

namespace MockGen.Setup
{
    internal interface IMethodSetup : IMethodSetupBase
    {
        void Execute(Action callback);
    }
}
