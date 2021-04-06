using MockGen.Setup;
using System;

namespace MockGen
{
    internal class ConcreteDependencyMethodsSetup
    {
        internal MethodSetupReturn<int> ICanBeMockedSetup { get; } = new MethodSetupReturn<int>();
        internal MethodSetupVoid<string> SaveFullName { get; } = new MethodSetupVoid<string>();

        internal void SetupDone()
        {
            ICanBeMockedSetup.SetupDone();
            SaveFullName.SetupDone();
        }
    }
}
