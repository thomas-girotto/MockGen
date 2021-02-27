namespace MockGen.Spy
{
    internal class MethodSpy
    {
        private int numberOfCalls;

        internal void WasCalled()
        {
            numberOfCalls++;
        }

        internal int NumberOfCalls => numberOfCalls;
    }
}
