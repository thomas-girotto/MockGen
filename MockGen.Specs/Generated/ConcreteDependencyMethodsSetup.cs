using MockGen.Setup;
using System;

namespace MockGen
{
    internal class ConcreteDependencyMethodsSetup
    {
        internal MethodSetupReturn<int> ICanBeMockedSetup { get; } = new MethodSetupReturn<int>();

        internal void SetupDone()
        {
            ICanBeMockedSetup.SetupDone();
        }
    }
}
