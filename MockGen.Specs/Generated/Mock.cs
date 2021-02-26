using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("MockGen.Benchmark")]
namespace MockGen.IDependencyNs
{
    internal class Mock<T>
    {
        internal static IDependencyMockBuilder Create()
        {
            return new IDependencyMockBuilder();
        }
    }
}
