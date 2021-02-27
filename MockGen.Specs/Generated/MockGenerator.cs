namespace MockGen
{
    public class MockGenerator
    {
        /// <summary>
        /// Gives type information to the compiler so that MockGen can generate a mock for T
        /// </summary>
        /// <typeparam name="T">Type for which we want to generate a mock</typeparam>
        public static void Generate<T>() { }
    }
}
