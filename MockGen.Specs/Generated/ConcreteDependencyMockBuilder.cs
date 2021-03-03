using MockGen.Setup;

namespace MockGen
{
    internal class ConcreteDependencyMockBuilder
    {
        private readonly ConcreteDependencyMethodsSetup methods = new ConcreteDependencyMethodsSetup();

        internal IMethodSetupReturn<int> ICanBeMocked()
        {
            return methods.ICanBeMockedSetup;
        }

        internal ConcreteDependencyMock Build()
        {
            methods.SetupDone();
            return new ConcreteDependencyMock(methods);
        }
    }
}
