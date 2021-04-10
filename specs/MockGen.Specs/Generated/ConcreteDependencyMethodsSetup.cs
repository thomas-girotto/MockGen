using MockGen.Setup;
using System;

namespace MockGen
{
    internal class ConcreteDependencyMethodsSetup
    {
        internal MethodSetupReturn<int> ICanBeMockedSetup { get; } = new MethodSetupReturn<int>();
        internal MethodSetupVoid<string> ICallProtectedMethodSetup { get; } = new MethodSetupVoid<string>();
        internal MethodSetupVoid<string> SaveFullName { get; } = new MethodSetupVoid<string>();

        internal void SetupDone()
        {
            ICanBeMockedSetup.SetupDone();
            ICallProtectedMethodSetup.SetupDone();
            SaveFullName.SetupDone();
        }
    }
}
