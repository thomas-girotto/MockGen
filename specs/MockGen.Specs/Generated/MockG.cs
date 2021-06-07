using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MockGen.Benchmark")]
namespace MockGen
{
    internal class MockG
    {
        /// <summary>
        /// Gives type information to the compiler so that MockGen can generate a mock for T
        /// </summary>
        /// <typeparam name="T">Type for which we want to generate a mock</typeparam>
        internal static Generate<T> Generate<T>() => new Generate<T>();
    }
}
