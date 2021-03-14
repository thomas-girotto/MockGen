using System;

namespace MockGen.Setup
{
    internal class MethodSetupVoid : MethodSetup
    {
        public void ExecuteSetup()
        {
            numberOfCalls++;
            currentConfiguration.RunActions();
        }
    }
}
