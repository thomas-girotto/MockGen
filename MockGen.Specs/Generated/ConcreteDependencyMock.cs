using MockGen.Setup;
using MockGen.Specs.Sut;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockGen
{
    internal class ConcreteDependencyMock : ConcreteDependency
    {
        private MethodSetupReturn<int> iCanBeMockedSetup;

        public ConcreteDependencyMock(MethodSetupReturn<int> iCanBeMockedSetup)
        {
            this.iCanBeMockedSetup = iCanBeMockedSetup;
        }

        public override int ICanBeMocked()
        {
            return iCanBeMockedSetup.ExecuteSetup();
        }
    }
}
