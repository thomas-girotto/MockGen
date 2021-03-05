using MockGen.Specs.Sut;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MockGen.Benchmark")]
namespace MockGen
{
    internal class Mock
    {
        internal static IDependencyMockBuilder IDependency()
        {
            return new IDependencyMockBuilder();
        }

        internal static ConcreteDependencyMockBuilder ConcreteDependency()
        {
            return new ConcreteDependencyMockBuilder();
        }

        internal static ConcreteDependencyMockBuilder ConcreteDependency(Model1 model1)
        {
            return new ConcreteDependencyMockBuilder(model1);
        }

        internal static ConcreteDependencyMockBuilder ConcreteDependency(Model1 model1, Model2 model2)
        {
            return new ConcreteDependencyMockBuilder(model1, model2);
        }
    }
}
