using MockGen.Setup;
using MockGen.Specs.Sut;

namespace MockGen
{
    internal class ConcreteDependencyMock : ConcreteDependency
    {
        private readonly ConcreteDependencyMethodsSetup methods;

        public ConcreteDependencyMock(ConcreteDependencyMethodsSetup methods)
        {
            this.methods = methods;
        }

        public override int ICanBeMocked()
        {
            return methods.ICanBeMockedSetup.ExecuteSetup();
        }
    }
}
