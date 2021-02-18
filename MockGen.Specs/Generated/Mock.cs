using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("MockGen.Benchmark")]
namespace MockGen.Specs.Generated
{
    internal class Mock<T>
    {
        internal static IDependencyMockBuilder IDependency()
        {
            return new IDependencyMockBuilder();
        }
    }
}
