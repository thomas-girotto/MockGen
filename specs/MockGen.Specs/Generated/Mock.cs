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

        internal static ITaskDependencyMockBuilder ITaskDependency()
        {
            return new ITaskDependencyMockBuilder();
        }

        internal static ConcreteDependencyMockBuilder ConcreteDependency()
        {
            return new ConcreteDependencyMockBuilder(false);
        }

        internal static ConcreteDependencyMockBuilder ConcreteDependency(bool callBase)
        {
            return new ConcreteDependencyMockBuilder(callBase);
        }

        internal static ConcreteDependencyMockBuilder ConcreteDependency(Model1 model1)
        {
            return new ConcreteDependencyMockBuilder(false, model1);
        }

        internal static ConcreteDependencyMockBuilder ConcreteDependency(bool callBase, Model1 model1)
        {
            return new ConcreteDependencyMockBuilder(callBase, model1);
        }

        internal static ConcreteDependencyMockBuilder ConcreteDependency(Model1 model1, Model2 model2)
        {
            return new ConcreteDependencyMockBuilder(false, model1, model2);
        }

        internal static ConcreteDependencyMockBuilder ConcreteDependency(bool callBase, Model1 model1, Model2 model2)
        {
            return new ConcreteDependencyMockBuilder(callBase, model1, model2);
        }
    }
}
