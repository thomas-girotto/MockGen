using MockGen.Setup;

namespace MockGen
{
    internal class ConcreteDependencyMockBuilder
    {
        private readonly MethodSetupReturn<int> iCanBeMockedSetup = new MethodSetupReturn<int>();
        
        internal IMethodSetupReturn<int> ICanBeMocked()
        {
            return iCanBeMockedSetup;
        }

        internal ConcreteDependencyMock Build()
        {
            return new ConcreteDependencyMock(iCanBeMockedSetup);
        }
    }
}
