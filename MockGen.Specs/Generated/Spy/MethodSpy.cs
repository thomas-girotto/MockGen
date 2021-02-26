namespace MockGen.Spy
{
    internal class MethodSpy
    {
        private int totalCalls;

        internal void WasCalled()
        {
            totalCalls++;
        }

        internal int TotalCalls => totalCalls;
    }
}
