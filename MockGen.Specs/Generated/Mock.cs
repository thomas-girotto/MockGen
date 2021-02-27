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
    }
}
