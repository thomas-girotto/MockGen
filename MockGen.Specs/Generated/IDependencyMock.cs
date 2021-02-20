using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Sut;

namespace MockGen.Specs.Generated.IDependencyNs
{
    internal class IDependencyMock : IDependency
    {
        private readonly MethodSetupVoid doSomethingSetup;
        private readonly MethodSetupVoid<int> doSomethingWithParameterSetup;
        private readonly MethodSetupReturn<int> getSomeNumberSetup;
        private readonly MethodSetupReturn<int, int> getSomeNumberWithParameterSetup;

        public IDependencyMock(MethodSetupVoid doSomethingSetup, MethodSetupVoid<int> doSomethingWithParameterSetup, MethodSetupReturn<int> getSomeNumberSetup, MethodSetupReturn<int, int> getSomeNumberWithParameterSetup)
        {
            this.doSomethingSetup = doSomethingSetup;
            this.doSomethingWithParameterSetup = doSomethingWithParameterSetup;
            this.getSomeNumberSetup = getSomeNumberSetup;
            this.getSomeNumberWithParameterSetup = getSomeNumberWithParameterSetup;
        }

        public void DoSomething()
        {
            doSomethingSetup.ExecuteSetup();
        }

        public void DoSomethingWithParameter(int input)
        {
            doSomethingWithParameterSetup.ExecuteSetup(input);
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
