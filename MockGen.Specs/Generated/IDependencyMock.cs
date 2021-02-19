using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Sut;

namespace MockGen.Specs.Generated.IDependencyNs
{
    internal class IDependencyMock : IDependency
    {
        private readonly MethodSetup doSomethingSetup;
        private readonly MethodSetup<int> getSomeNumberSetup;
        private readonly MethodSetup<int, int> getSomeNumberWithParameterSetup;

        public IDependencyMock(MethodSetup doSomethingSetup, MethodSetup<int> getSomeNumberSetup, MethodSetup<int, int> getSomeNumberWithParameterSetup)
        {
            this.doSomethingSetup = doSomethingSetup;
            this.getSomeNumberSetup = getSomeNumberSetup;
            this.getSomeNumberWithParameterSetup = getSomeNumberWithParameterSetup;
        }

        public void DoSomething()
        {
            doSomethingSetup.ExecuteSetup();
        }

        public int GetSomeNumber()
        {
            return getSomeNumberSetup.ExecuteSetup();
        }

        public int GetSomeNumberWithParameter(int input)
        {
            return getSomeNumberWithParameterSetup.ExecuteSetup(input);
        }
    }
}
