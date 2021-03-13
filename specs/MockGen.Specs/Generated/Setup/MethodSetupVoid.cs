using System;

namespace MockGen.Setup
{
    internal class MethodSetupVoid : MethodSetup, IMethodSetup
    {
        public void ExecuteSetup()
        {
            numberOfCalls++;
            currentConfiguration.RunActions();
        }
    }
}
