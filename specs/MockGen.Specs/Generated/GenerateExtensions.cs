using MockGen.Specs.Sut;

namespace MockGen
{
    internal static class GenerateExtensions
    {
        internal static IDependencyMockBuilder New(this Generate<IDependency> _) => new IDependencyMockBuilder();
        internal static ITaskDependencyMockBuilder New(this Generate<ITaskDependency> _) => new ITaskDependencyMockBuilder();
        internal static ConcreteDependencyMockBuilder New(this Generate<ConcreteDependency> _) => new ConcreteDependencyMockBuilder(false);
        internal static ConcreteDependencyMockBuilder New(this Generate<ConcreteDependency> _, bool callBase) => new ConcreteDependencyMockBuilder(callBase);
        internal static ConcreteDependencyMockBuilder New(this Generate<ConcreteDependency> _, Model1 model1) => new ConcreteDependencyMockBuilder(false, model1);
        internal static ConcreteDependencyMockBuilder New(this Generate<ConcreteDependency> _, bool callBase, Model1 model1) => new ConcreteDependencyMockBuilder(callBase, model1);
        internal static ConcreteDependencyMockBuilder New(this Generate<ConcreteDependency> _, Model1 model1, Model2 model2) => new ConcreteDependencyMockBuilder(false, model1, model2);
        internal static ConcreteDependencyMockBuilder New(this Generate<ConcreteDependency> _, bool callBase, Model1 model1, Model2 model2) => new ConcreteDependencyMockBuilder(callBase, model1, model2);
    }
}
